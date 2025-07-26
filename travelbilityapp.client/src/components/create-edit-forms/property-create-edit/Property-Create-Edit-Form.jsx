import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useState } from 'react';
import { Button, Col, Container, Form, Row, Spinner } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import toast from "react-hot-toast";
import { useNavigate } from 'react-router-dom';

import { usePropertyForEditContext } from '../../../contexts/Property-For-Edit-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getPropertyFacilities } from '../../../services/facilitiesService';
import { create, edit } from '../../../services/propertiesService';
import { getAll as getPropertyTypes } from '../../../services/typesServices';
import { constructPropertyDataForEditing, formatErrorsData } from '../../../utils/property-utils';
import { propertySchema } from '../../../validations';

import { WheelchairTireSpinner } from '../../loaders/Loaders';
import BasicInformation from './Basic-Information';
import Facilities from '../common/Facilities';
import Photos from '../common/Photos';

import styles from './Property-Create-Edit-Form.module.css';

export default function PropertyCreateEditForm() {
    const { data: propertyTypes, isDataLoaded: isPropertyTypesLoaded } = useBasicGetFetch(() => getPropertyTypes());
    const { data: facilities } = useBasicGetFetch(() => getPropertyFacilities());

    const { propertyData } = usePropertyForEditContext();

    const [menualErrors, setMenualErrors] = useState({});
    const [step, setStep] = useState(1);

    const navigate = useNavigate();

    const { control, formState: { errors, isSubmitting }, getValues, handleSubmit, register, reset, trigger, watch } = useForm({
        defaultValues: {
            "step-2": {
                commonFacilityIds: [],
                accessibilityIds: []
            }
        },
        mode: 'onChange',
        resolver: yupResolver(propertySchema,),
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
            photoUrls: data["step-3"].photoUrls.map(iu => iu.url),
        };
        debugger

        try {
            const savedPropertyData = propertyData.id
                ? await edit(propertyData.id, propertyDataToSave)
                : await create(propertyDataToSave);

            toast.success(`You have successfully ${propertyData.id === '' ? "listed" : "edited"} your property.`);
            navigate(`/my-properties/${savedPropertyData.id}`);
        } catch (errorInfo) {
            const errorsData = formatErrorsData(getValues(), errorInfo.errorsData);

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
            <div className="d-flex justify-content-center">

                <div className={styles["list-property-nav"]}>
                    <span><i className="fas fa-hotel text-primary"></i> Basic information</span>
                    <span><i className="fab fa-accessible-icon text-primary"></i> Facilities</span>
                    <span><i className="far fa-images text-primary"></i> Photos</span>
                </div>
            </div>

            <Row className="d-flex justify-content-center">
                <Col lg={9} className={styles["list-property-form"]}>
                    {isPropertyTypesLoaded
                        ? <Form onSubmit={handleSubmit(createHandler)}>

                            {step === 1 && <BasicInformation
                                propertyTypes={propertyTypes}
                                register={register}
                                errors={{ ...errors["step-1"], ...menualErrors["step-1"] }}
                            />}

                            {step === 2 && <Facilities
                                facilities={facilities}
                                hasAccessibility={true}
                                watch={watch}
                                register={register}
                                errors={{ ...errors["step-2"], ...menualErrors["step-2"] }}
                            />}

                            {step === 3 && <Photos
                                control={control}
                                errors={{ ...errors["step-3"], ...menualErrors["step-3"], imageUrl: menualErrors.imageUrl }}
                                updateMenualErrorsHandler={updateMenualErrorsHandler}
                                isDisabled={isSubmitting}
                            />}

                            <div className={`d-flex justify-content-${step === 1 ? "end" : "between"} mt-3`}>
                                {step > 1 && <Button type="button" onClick={prevStep} className="me-2">Back</Button>}
                                {step <= 2 && <Button type="button" onClick={nextStep}>Next</Button>}
                                {step == 3 &&
                                    < Button type="submit" disabled={isSubmitting}>
                                        {isSubmitting
                                            ? <>
                                                <Spinner
                                                    as="span"
                                                    animation="border"
                                                    size="sm"
                                                    role="status"
                                                    aria-hidden="true"
                                                />
                                                Saving...
                                            </>
                                            : <span>Save and add rooms</span>
                                        }
                                    </Button>
                                }
                            </div>
                        </Form>
                        : <WheelchairTireSpinner />
                    }
                </Col>
            </Row >
        </Container>
    );
};
