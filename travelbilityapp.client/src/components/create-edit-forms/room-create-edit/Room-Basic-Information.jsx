import { Form, Row, Col } from 'react-bootstrap';

export default function RoomBasicInformation({
    roomTypes,
    bedTypes,
    errors,
    register
}) {
    return (
        <>
            <h4 className="mb-3 text-center text-primary">Basic information</h4>

            <Row>
                <Col sm={4} md={4} lg={4}>
                    <Form.Group className="mb-3">
                        <Form.Label>Type</Form.Label>
                        <Form.Select
                            defaultValue=""
                            className="custom-select px-4"
                            {...register("step-1.roomTypeId")}
                        >
                            <option value="" disabled>--- Select ---</option>
                            {roomTypes.map(rt => (
                                <option key={rt.id} value={rt.id}>{rt.name}</option>
                            ))}
                        </Form.Select>
                        {errors?.roomTypeId
                            ? <p className="text-danger">{errors?.roomTypeId.message}</p>
                            : (errors?.RoomTypeId && errors.RoomTypeId.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                        }
                    </Form.Group>
                </Col>

                <Col sm={4} md={4} lg={4}>
                    <Form.Group className="mb-3">
                        <Form.Label>Maximum guest count</Form.Label>
                        <Form.Select
                            defaultValue=""
                            className="custom-select px-4"
                            {...register("step-1.maxGuests")}
                        >
                            <option value="" disabled>--- Select ---</option>
                            {[1, 2, 3, 4, 5, 6].map(g => (
                                <option key={g} value={g}>
                                    {`${g} guest${g > 1 ? "s" : ""}`}
                                </option>
                            ))}
                        </Form.Select>
                        {errors?.maxGuests
                            ? <p className="text-danger">{errors?.maxGuests.message}</p>
                            : (errors?.MaxGuests && errors.MaxGuests.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                        }
                    </Form.Group>
                </Col>

                <Col sm={4} md={4} lg={4}>
                    <Form.Group className="mb-3">
                        <Form.Label>Price per night</Form.Label>
                        <Form.Control
                            placeholder="ex. 00.00"
                            {...register("step-1.pricePerNight")} />
                            {errors?.pricePerNight
                            ? <p className="text-danger">{errors?.pricePerNight.message}</p>
                            : (errors?.PricePerNight && errors.PricePerNight.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                            }
                    </Form.Group>
                </Col>
            </Row>

            <Row>
                <Col sm={4} md={4} lg={4}>
                    <Form.Group className="mb-3">
                        <Form.Label>Main bed type</Form.Label>
                        <Form.Select
                            defaultValue=""
                            className="custom-select px-4"
                            {...register("step-1.mainBedTypeId")}
                        >
                            <option value="" disabled>--- Select ---</option>
                            {bedTypes.map(bt => (
                                <option key={bt.id} value={bt.id}>{bt.name}</option>
                            ))}
                        </Form.Select>
                        {errors?.mainBedTypeId
                            ? <p className="text-danger">{errors?.mainBedTypeId.message}</p>
                            : (errors?.MainBedTypeId && errors.MainBedTypeId.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                        }
                    </Form.Group>
                </Col>

                <Col sm={4} md={4} lg={4}>
                    <Form.Group className="mb-3">
                        <Form.Label>Room size (м²)</Form.Label>
                        <Form.Control
                            placeholder="ex. 5"
                            {...register("step-1.size")} />
                        {errors?.size
                            ? <p className="text-danger">{errors?.size.message}</p>
                            : (errors?.Size && errors.Size.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                        }
                    </Form.Group>
                </Col>

                <Col sm={4} md={4} lg={4}>
                    <Form.Group className="mb-3">
                        <Form.Label>Number of Rooms of this type</Form.Label>
                        <Form.Control
                            type="number"
                            min="1"
                            {...register("step-1.numberOfUnits")} />
                        {errors?.numberOfUnits
                            ? <p className="text-danger">{errors?.numberOfUnits.message}</p>
                            : (errors?.NumberOfUnits && errors.NumberOfUnits.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                        }
                    </Form.Group>
                </Col>
            </Row>

            <Form.Group className="mb-3">
                <Form.Label>Description</Form.Label>
                <Form.Control as="textarea" rows={7} {...register("step-1.description")} />
                {errors?.description
                    ? <p className="text-danger">{errors?.description.message}</p>
                    : (errors?.Description && errors.Description.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                }
            </Form.Group>
        </>
    );
}