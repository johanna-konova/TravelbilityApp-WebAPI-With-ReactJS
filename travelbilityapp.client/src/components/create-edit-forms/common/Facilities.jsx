import { useEffect, useState } from 'react';
import { Form, Row } from 'react-bootstrap';

import styles from './Common-Styles.module.css';

export default function Facilities({
    facilities,
    hasAccessibility,
    errors,
    register,
    watch
}) {
    const [commonFacilities, setCommonFacilities] = useState([]);
    const [accessibility, setAccessibility] = useState([]);

    useEffect(() => {
        setCommonFacilities(facilities.filter(f => f.isForAccessibility === false).map(f => ({ ...f, id: String(f.id) })));
        setAccessibility(facilities.filter(f => f.isForAccessibility).map(f => ({ ...f, id: String(f.id) })));
    }, [facilities]);

    return (
            <Row className="d-flex justify-content-center">
                <Form.Group className="mb-3 w-100">
                    <Row className={`justify-content-around ${styles["facilities"]}`}>
                        <div>
                            <h4 className="text-primary">Facilities</h4>
                            <div className={styles["facilities-scrollbox"]}>
                                {commonFacilities.map(cf => (
                                    <label key={cf.id} className={styles["custom-checkbox"]}>
                                        <input
                                            type="checkbox"
                                            value={cf.id}
                                            checked={watch("step-2.commonFacilityIds")?.includes(cf.id)}
                                            {...register("step-2.commonFacilityIds")}
                                        />
                                        <span className={styles["checkmark"]}></span>
                                        <span>{cf.name}</span>
                                    </label>
                                ))}
                            </div>
                            {errors?.commonFacilityIds
                                ? <p className="text-danger">{errors?.commonFacilityIds.message}</p>
                                : (errors?.CommonFacilityIds && errors.CommonFacilityIds.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                            }
                        </div>
                        {hasAccessibility &&
                            <div>
                                <h4 className="text-primary">Accessibility</h4>
                                {accessibility.map(a => (
                                    <label key={a.id} className={styles["custom-checkbox"]}>
                                        <input
                                            type="checkbox"
                                            value={a.id}
                                            checked={watch("step-2.accessibilityIds")?.includes(a.id)}
                                            {...register("step-2.accessibilityIds")}
                                        />
                                        <span className={styles["checkmark"]}></span>
                                        <span>{a.name}</span>
                                    </label>
                                ))}
                                {errors?.accessibilityIds
                                    ? <p className="text-danger">{errors?.accessibilityIds.message}</p>
                                    : (errors?.AccessibilityIds && errors.AccessibilityIds.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                                }
                            </div>
                        }
                    </Row>
                </Form.Group>
            </Row>
    );
};
