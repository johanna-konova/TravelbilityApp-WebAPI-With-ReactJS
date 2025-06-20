import { Form, Button, Col, Row } from 'react-bootstrap';

import styles from './Property-Create-Edit-Form.module.css';

export default function PropertyCreateEditFormStepOne({
    propertyTypes,
    register,
    errors,
    nextStepHandler
}) {
    return (
        <Row className="d-flex justify-content-center">
            <Col lg={9} className={styles["list-property-form"]}>

                <Row>
                    <Col lg={7}>
                        <Form.Group className="mb-3">
                            <Form.Label>Name</Form.Label>
                            <Form.Control {...register("step-1.name")} />
                            {errors?.name 
                                ? <p className="text-danger">{errors?.name.message}</p>
                                : (errors?.Name && errors.Name.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                            }
                        </Form.Group>
                    </Col>

                    <Col lg={5}>
                        <Form.Group className="mb-3">
                            <Form.Label>Type</Form.Label>
                            <Form.Select
                                defaultValue=""
                                className="custom-select px-4"
                                {...register("step-1.typeId")}
                            >
                                <option value="" disabled>--- Select ---</option>
                                {propertyTypes.map(pt => (
                                    <option key={pt.id} value={pt.id}>{pt.name}</option>
                                ))}
                            </Form.Select>
                            {errors?.typeId
                                ? <p className="text-danger">{errors?.typeId.message}</p>
                                : (errors?.TypeId && errors.TypeId.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                            }
                        </Form.Group>
                    </Col>
                </Row>

                <Row>
                    <Col lg={4}>
                        <Form.Group className="mb-3">
                            <Form.Label>Count of stars</Form.Label>
                            <Form.Select
                                defaultValue=""
                                className="custom-select px-4"
                                {...register("step-1.starsCount")}
                            >
                                <option value="" disabled>--- Select ---</option>
                                {[5, 4, 3, 2, 1].map(s => (
                                    <option key={s} value={s}>
                                        {`${s} star${s > 1 ? "s" : ""}`}
                                    </option>
                                ))}
                            </Form.Select>
                            {errors?.StarsCount && errors.StarsCount.map((message, index) => <div key={index} className="text-danger">{message}</div>)}
                        </Form.Group>
                    </Col>

                    <Col lg={4}>
                        <Form.Group className="mb-3">
                            <Form.Label>Check-in</Form.Label>
                            <Form.Control
                                placeholder="HH:mm"
                                {...register("step-1.checkIn")}
                            />
                            {errors?.checkIn
                                ? <p className="text-danger">{errors?.checkIn.message}</p>
                                : (errors?.CheckIn && errors.CheckIn.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                            }
                        </Form.Group>
                    </Col>

                    <Col lg={4}>
                        <Form.Group className="mb-3">
                            <Form.Label>Check-out</Form.Label>
                            <Form.Control
                                placeholder="HH:mm"
                                {...register("step-1.checkOut")}
                            />
                            {errors?.checkOut
                                ? <p className="text-danger">{errors?.checkOut.message}</p>
                                : (errors?.CheckOut && errors.CheckOut.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                            }
                        </Form.Group>
                    </Col>
                </Row>

                <Form.Group className="mb-3">
                    <Form.Label>Адрес</Form.Label>
                    <Form.Control {...register("step-1.address")} />
                    {errors?.address
                        ? <p className="text-danger">{errors?.address.message}</p>
                        : (errors?.Address && errors.Address.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                    }
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Описание</Form.Label>
                    <Form.Control as="textarea" rows={7} {...register("step-1.description")} />
                    {errors?.description
                        ? <p className="text-danger">{errors?.description.message}</p>
                        : (errors?.Description && errors.Description.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                    }
                </Form.Group>

                <div className="d-flex justify-content-end mt-3">
                    <Button type="button" onClick={nextStepHandler}>Next</Button>
                </div>
            </Col>
        </Row>
    );
};
