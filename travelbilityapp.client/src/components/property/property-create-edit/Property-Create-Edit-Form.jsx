import  { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Container, Form } from 'react-bootstrap';
import toast from "react-hot-toast";

//import { usePropertyContext } from '../../../contexts/Property-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getAll as getPropertyTypes } from '../../../services/typesServices';
import { getFacilities } from '../../../services/facilitiesService';
//import { create, edit } from '../../../services/propertiesService';
//import { createPropertyFacility, deletePropertyFacility } from '../../../services/propertiesFacilitiesService';
import { propertySchema } from '../../../validations';
//import { constructPropertyDataForEditing } from '../../../utils/property-utils';

import PropertyCreateEditFormStepOne from './Property-Create-Edit-Form-Step-One';
import PropertyCreateEditFormStepTwo from './Property-Create-Edit-Form-Step-Two';
import PropertyCreateEditFormStepThree from './Property-Create-Edit-Form-Step-Three';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

import styles from './Property-Create-Edit-Form.module.css';

export default function PropertyCreateEditForm() {
    const { data: propertyTypes, isDataLoaded: isPropertyTypesLoaded } = useBasicGetFetch(() => getPropertyTypes());
    const { data: facilities } = useBasicGetFetch(() => getFacilities());

    //const { propertyData, propertyFacilities } = usePropertyContext();

    const [menualErrors, setMenualErrors] = useState({});
    const [step, setStep] = useState(1);

    const navigate = useNavigate();

    const { register, control, handleSubmit, trigger, formState: { errors, isSubmitting }, reset, watch } = useForm({
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

    /*useEffect(() => {
        if (propertyData._id !== undefined) {
            reset(constructPropertyDataForEditing(propertyData, propertyFacilities));
        }
    }, [propertyData, propertyFacilities, reset]);*/

    const updateMenualErrorsHandler = (errors) =>
        setMenualErrors(previousMenualErrors => ({ ...previousMenualErrors, ...errors }));

    const nextStep = async () => {
        const isStepValid = await trigger(`step-${step}`);

        if (isStepValid) {
            setStep(step + 1);
        }
    };

    const prevStep = () => setStep(step - 1);

    const createHandler = async (data) => {
        const propertyData = {
            ...data["step-1"],
            imageUrls: data["step-3"].imageUrls.map(iu => iu.url)
        };

        /*const savedPropertyData = data.id
            ? await edit(data.id, propertyData)
            : await create(propertyData);

        const facilityIds = [...data["step-2"].commonFacilityIds, ...data["step-2"].accessibilityIds];

        const propertyFacilitiesToDelete = propertyFacilities
            .filter(pf => facilityIds.includes(pf.facilityId) === false)
            .map(pf => pf.recordId);

        for (const facilityId of facilityIds) {
            if (propertyFacilities.some(pf => pf.facilityId === facilityId) === false) {
                await createPropertyFacility({
                    propertyId: savedPropertyData._id,
                    facilityId
                });
            }
        }

        for (const propertyFacilityToDelete of propertyFacilitiesToDelete) {
            await deletePropertyFacility(propertyFacilityToDelete);
        }

        toast.success(`You have successfully ${data.id === undefined ? "listed" : "edited"} your property.`);
        navigate("/my-properties");*/
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

                    <input type="hidden" {...register("id")} />

                    {step === 1 && <PropertyCreateEditFormStepOne
                        propertyTypes={propertyTypes}
                        register={register}
                        errors={errors["step-1"]}
                        nextStepHandler={nextStep}
                    />}

                    {step === 2 && <PropertyCreateEditFormStepTwo
                        facilities={facilities}
                        watch={watch}
                        register={register}
                        errors={errors["step-2"]}
                        previousStepHandler={prevStep}
                        nextStepHandler={nextStep}
                    />}

                    {step === 3 && <PropertyCreateEditFormStepThree
                        control={control}
                        errors={{ ...errors["step-3"], ...menualErrors }}
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
