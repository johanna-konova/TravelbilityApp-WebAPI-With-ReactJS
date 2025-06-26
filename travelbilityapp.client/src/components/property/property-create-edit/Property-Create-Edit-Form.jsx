import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useState } from 'react';
import { Container, Form } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import toast from "react-hot-toast";
import { useNavigate } from 'react-router-dom';

import { usePropertyContext } from '../../../contexts/Property-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getFacilities } from '../../../services/facilitiesService';
import { create, edit } from '../../../services/propertiesService';
import { getAll as getPropertyTypes } from '../../../services/typesServices';
import { constructPropertyDataForEditing, formatCreatePropertyErrorsData } from '../../../utils/property-utils';
import { propertySchema } from '../../../validations';

import { WheelchairTireSpinner } from '../../loaders/Loaders';
import PropertyCreateEditFormStepOne from './Property-Create-Edit-Form-Step-One';
import PropertyCreateEditFormStepThree from './Property-Create-Edit-Form-Step-Three';
import PropertyCreateEditFormStepTwo from './Property-Create-Edit-Form-Step-Two';

import styles from './Property-Create-Edit-Form.module.css';

export default function PropertyCreateEditForm() {
    const { data: propertyTypes, isDataLoaded: isPropertyTypesLoaded } = useBasicGetFetch(() => getPropertyTypes());
    const { data: facilities } = useBasicGetFetch(() => getFacilities());

    const { propertyData } = usePropertyContext();

    const [menualErrors, setMenualErrors] = useState({});
    const [step, setStep] = useState(1);

    const navigate = useNavigate();

    const { register, control, handleSubmit, trigger, formState: { errors, isSubmitting }, reset, watch, getValues } = useForm({
        defaultValues: {
            "step-2": {
                commonFacilityIds: [],
                accessibilityIds: []
            }
            //rooms: []
        },
        mode: 'onChange',
        resolver: yupResolver(propertySchema),
    });

    useEffect(() => {
        if (propertyData.id !== undefined) {
            reset(constructPropertyDataForEditing(propertyData));
        }
    }, [propertyData, reset]);

    const updateMenualErrorsHandler = (errors) => {
        if (errors.ImageUrls) {
            setMenualErrors(previousMenualErrors => ({
                ...previousMenualErrors,
                "step-3": {
                    ...previousMenualErrors["step-3"],
                    ImageUrls: errors.ImageUrls
                },
            }));
        } else {
            setMenualErrors(previousMenualErrors => ({ ...previousMenualErrors, ...errors }));
        }
    }

    const nextStep = async () => {
        const isStepValid = await trigger(`step-${step}`);

        if (isStepValid) {
            setStep(step + 1);
        }
    };

    const prevStep = () => setStep(step - 1);

    const createHandler = async (data) => {
        const propertyDataToSave = {
            ...data["step-1"],
            facilityIds: [...data["step-2"].commonFacilityIds, ...data["step-2"].accessibilityIds],
            photoUrls: data["step-3"].photoUrls.map(iu => iu.url)
        };
        debugger

        try {
            const savedPropertyData = propertyData.id
                ? await edit(propertyData.id, propertyDataToSave)
                : await create(propertyDataToSave);

            toast.success(`You have successfully ${propertyData.id === '' ? "listed" : "edited"} your property.`);
            navigate(`/properties/${savedPropertyData.id}`);
        } catch (errorInfo) {
            const errorsData = formatCreatePropertyErrorsData(getValues(), errorInfo.errorsData);

            updateMenualErrorsHandler(errorsData);

            if (Object.keys(errorsData["step-1"]).length > 0) {
                setStep(1);
            } else if (Object.keys(errorsData["step-2"]).length > 0) {
                setStep(2);
            } else if (Object.keys(errorsData["step-3"]).length > 0) {
                setStep(3);
            }
        }
    };

    return (
        <Container className="mt-3">
            {isPropertyTypesLoaded
                ? <Form onSubmit={handleSubmit(createHandler)}>

                    <div className="d-flex justify-content-center">
                        <div className={styles["list-property-nav"]}>
                            <span><i className="fas fa-hotel text-primary"></i> Basic information</span>
                            <span><i className="fab fa-accessible-icon text-primary"></i> Facilities</span>
                            <span><i className="far fa-images text-primary"></i> Photos</span>
                        </div>
                    </div>

                    {step === 1 && <PropertyCreateEditFormStepOne
                        propertyTypes={propertyTypes}
                        register={register}
                        errors={{ ...errors["step-1"], ...menualErrors["step-1"] }}
                        nextStepHandler={nextStep}
                    />}

                    {step === 2 && <PropertyCreateEditFormStepTwo
                        facilities={facilities}
                        watch={watch}
                        register={register}
                        errors={{ ...errors["step-2"], ...menualErrors["step-2"] }}
                        previousStepHandler={prevStep}
                        nextStepHandler={nextStep}
                    />}

                    {step === 3 && <PropertyCreateEditFormStepThree
                        control={control}
                        errors={{ ...errors["step-3"], ...menualErrors["step-3"], imageUrl: menualErrors.imageUrl }}
                        updateMenualErrorsHandler={updateMenualErrorsHandler}
                        previousStepHandler={prevStep}
                        isSaving={isSubmitting}
                    />}
                </Form>
                : <WheelchairTireSpinner />
            }
        </Container>
    );
};
