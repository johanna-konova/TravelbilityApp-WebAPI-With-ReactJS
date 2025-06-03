import { useEffect, useState } from 'react';
import { Form, Button, Col, Row } from 'react-bootstrap';

import styles from './Property-Create-Edit-Form.module.css';

export default function PropertyCreateEditFormStepTwo({
    facilities,
    watch,
    register,
    errors,
    previousStepHandler,
    nextStepHandler
}) {
    const [commonFacilities, setCommonFacilities] = useState([]);
    const [accessibility, setAccessibility] = useState([]);

    useEffect(() => {
        setCommonFacilities(facilities.filter(f => f.isForAccessibility === false));
        setAccessibility(facilities.filter(f => f.isForAccessibility));
    }, [facilities]);

    return (
        <Row className="d-flex justify-content-center">
            <Col lg={9} className={styles["list-property-form"]}>
                <Form.Group className="mb-3">
                    <Row className={styles["facilities"]} >
                        <Col lg={6}>
                            <h4 className="text-primary">Facilities</h4>
                            {commonFacilities.map(cf => (
                                <label key={cf.id} className={styles["custom-checkbox"]}>
                                    <input
                                        type="checkbox"
                                        value={cf.id}
                                        //checked={watch("step-2.commonFacilityIds")?.includes(cf.id)}
                                        {...register("step-2.commonFacilityIds")}
                                    />
                                    <span className={styles["checkmark"]}></span>
                                    <span>{cf.name}</span>
                                </label>
                            ))}
                            {errors?.commonFacilityIds && <p className="text-danger">{errors?.commonFacilityIds.message}</p>}
                        </Col>
                        <Col lg={6}>
                            <h4 className="text-primary">Accessibility</h4>
                            {accessibility.map(a => (
                                <label key={a.id} className={styles["custom-checkbox"]}>
                                    <input
                                        type="checkbox"
                                        value={a.id}
                                        //checked={watch("step-2.accessibilityIds")?.includes(a.id)}
                                        {...register("step-2.accessibilityIds")}
                                    />
                                    <span className={styles["checkmark"]}></span>
                                    <span>{a.name}</span>
                                </label>
                            ))}
                            {errors?.accessibilityIds && <p className="text-danger">{errors?.accessibilityIds.message}</p>}
                        </Col>
                    </Row>
                </Form.Group>

                <div className="d-flex justify-content-between mt-3">
                    <Button type="button" onClick={previousStepHandler} className="me-2">Back</Button>
                    <Button type="button" onClick={nextStepHandler}>Next</Button>
                </div>
            </Col>
        </Row>
    );
};
