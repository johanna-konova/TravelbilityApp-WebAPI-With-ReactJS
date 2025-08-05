import { yupResolver } from '@hookform/resolvers/yup';
import React, { useEffect, useState } from 'react';
import { Button, Form, Spinner } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import toast from "react-hot-toast";

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getAll as getBedTypes } from '../../../services/bedTypesService';
import { getRoomFacilities } from '../../../services/facilitiesService';
import { getAll as getRoomTypes } from '../../../services/roomTypesService';
import { create, edit, getForEditById } from '../../../services/roomsService';
import { IsSelectedRoomAccessible, constructRoomDataForEditing, formatErrorsData } from '../../../utils/property-utils';
import { roomSchema } from '../../../validations';
import CancelModal from '../../modals/Cancel-Modal';
import RoomBasicInformation from './Room-Basic-Information';
import Facilities from '../common/Facilities';
import Photos from '../common/Photos';

export default function AddRoomModal({
    roomId,
    propertyId,
    addRoomHandler,
    closeModalHandler
}) {
    const { data: facilities } = useBasicGetFetch(() => getRoomFacilities());
    const { data: roomTypes } = useBasicGetFetch(() => getRoomTypes());
    const { data: bedTypes } = useBasicGetFetch(() => getBedTypes());

    const [menualErrors, setMenualErrors] = useState({});
    const [step, setStep] = useState(1);
    const [isAccessibleRoomSelected, setIsAccessibleRoomSelected] = useState(false);
    const [isCancelModalShowed, setIsCancelModalShowed] = useState(false);

    const { control, formState: { errors, isSubmitting }, getValues, handleSubmit, register, reset, setError, setValue, trigger, watch } = useForm({
        defaultValues: {
            "step-2": {
                commonFacilityIds: [],
                accessibilityIds: []
            },
        },
        mode: 'onChange',
        resolver: yupResolver(roomSchema),
    });

    useEffect(() => {
        if (roomId !== undefined) {
            (async () => {
                const roomDataToEdit = await getForEditById(roomId, propertyId);
                reset(constructRoomDataForEditing(roomDataToEdit));
            }
            )()
        }
    }, [roomId]);

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

    const hideCancelModalHandler = () => setIsCancelModalShowed(false);

    const nextStep = async () => {
        const selectedTypeId = getValues("step-1.roomTypeId");
        const isSelectedRoomAccessible = IsSelectedRoomAccessible(roomTypes, selectedTypeId);

        if (step === 2) {
            if (isSelectedRoomAccessible === false) {
                setValue("step-2.accessibilityIds", []);
            } else {
                const selectedAccessibilityIds = getValues("step-2.accessibilityIds");

                if (selectedAccessibilityIds.length === 0) {
                    setError("step-2.accessibilityIds", { message: "Please, select at least 1 Accessibility." });
                    return;
                }
            }
        }

        const isStepValid = await trigger(`step-${step}`);

        if (isStepValid) {
            setIsAccessibleRoomSelected(isSelectedRoomAccessible);
            setStep(step + 1);
        }
    };

    const prevStep = () => setStep(step - 1);

    const createHandler = async (data) => {
        const roomDataToSave = {
            propertyId,
            ...data["step-1"],
            facilityIds: [...data["step-2"].commonFacilityIds, ...data["step-2"].accessibilityIds],
            photoUrls: data["step-3"].photoUrls.map(iu => iu.url),
        };
        debugger

        try {
            const savedRoomData = roomId
                ? await edit(roomId, roomDataToSave)
                : await create(roomDataToSave);

            toast.success(`You have successfully ${roomId === undefined ? "added" : "edited"} the room.`, { position: "buttom-right" });

            addRoomHandler(savedRoomData);
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
        <div className="custom-modal-overlay">
            <div
                className="custom-modal-content"
                style={{ display: isCancelModalShowed ? "none" : "block" }}
            >
                <div className="x-close">
                    <span onClick={() => setIsCancelModalShowed(true)}>X</span>
                </div>
                <Form onSubmit={handleSubmit(createHandler)}>
                    {step === 1 &&
                        <RoomBasicInformation
                            roomTypes={roomTypes}
                            bedTypes={bedTypes}
                            errors={{ ...errors["step-1"], ...menualErrors["step-1"] }}
                            register={register}
                        />
                    }

                    {step === 2 &&
                        <Facilities
                            facilities={facilities}
                            hasAccessibility={isAccessibleRoomSelected}
                            errors={{ ...errors["step-2"], ...menualErrors["step-2"] }}
                            register={register}
                            watch={watch}
                        />
                    }

                    {step === 3 &&
                        <Photos
                            errors={{ ...errors["step-3"], ...menualErrors["step-3"], imageUrl: menualErrors.imageUrl }}
                            updateMenualErrorsHandler={updateMenualErrorsHandler}
                            isDisabled={isSubmitting}
                            control={control}
                        />
                    }

                    <div className="d-flex justify-content-between mt-3">
                        <div className="d-flex">
                            {step > 1 && <Button type="button" onClick={prevStep} className="me-2">Back</Button>}
                            <Button type="button" variant="info" onClick={() => setIsCancelModalShowed(true)}>Cancel</Button>
                        </div>
                        {step < 3 && <Button type="button" onClick={nextStep}>Next</Button>}
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
                                    : <span>Save</span>
                                }
                            </Button>
                        }
                    </div>
                </Form>
            </div>

            <CancelModal
                isModalShowed={isCancelModalShowed}
                closeModalHandler={hideCancelModalHandler}
                discardChangesHandler={closeModalHandler}
            />
        </div>
    );
};