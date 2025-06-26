import  { useState } from 'react';
import { Form, Button, Col, Row, Image, Spinner } from 'react-bootstrap';
import { useFieldArray } from 'react-hook-form';

import { imageUrlSchema } from '../../../validations';

import styles from './Property-Create-Edit-Form.module.css';

export default function PropertyCreateEditFormStepThree({
    control,
    errors,
    updateMenualErrorsHandler,
    previousStepHandler,
    isSaving
}) {
    const [currentImageUrl, setCurrentImageUrl] = useState('');

    const { fields: photoUrls, append, remove } = useFieldArray({
        control,
        name: 'step-3.photoUrls'
    });

    const uploadImageHandler = async () => {
        const trimmedCurrentImageUrl = currentImageUrl.trim();

        try {
            await imageUrlSchema.validate(trimmedCurrentImageUrl);

            if (photoUrls.some(iu => iu.url === trimmedCurrentImageUrl)) {
                throw new Error("You have already added this photo. Please, upload a new URL.");
            }

            append({ url: trimmedCurrentImageUrl });
            setCurrentImageUrl("");
            updateMenualErrorsHandler({ imageUrl: "" });
        } catch (error) {
            updateMenualErrorsHandler({ imageUrl: error.message });
        }
    };

    const removeImageHandler = (index) => {
        if (errors.ImageUrls && errors.ImageUrls[index] != undefined) {
            delete errors.ImageUrls[index];

            const reIndexedErrors = Object.entries(errors.ImageUrls)
                .reduce((acc, [k, v]) => {
                    if (k < index) {
                        acc[k] = v;
                    } else {
                        acc[k - 1] = v;
                    }

                    return acc;
                }, {});

            updateMenualErrorsHandler({ ImageUrls: reIndexedErrors });
        }

        remove(index);
    };

    return (
        <Row className="d-flex justify-content-center">
            <Col lg={9} className={styles["list-property-form"]}>

                <Row className="text-center">
                    <Col>
                        <h4>Upload at least 5 photo URLs</h4>
                        <p>jpg/jpeg or png</p>
                    </Col>
                </Row>

                <Form.Label>Photo URL</Form.Label>

                {/* Полето за въвеждане на URL и бутонът за добавяне */}
                <div className="mb-3">
                    <Row>
                        <Col md={10} className="pr-0">
                            <Form.Group controlId="imageUrlInput">
                                <Form.Control
                                    type="text"
                                    placeholder="https://example.com/image.jpg"
                                    value={currentImageUrl}
                                    disabled={isSaving}
                                    onChange={e => setCurrentImageUrl(e.target.value)}
                                />
                            </Form.Group>
                        </Col>
                        <Col md={2} className="d-flex align-items-end">
                            <Button variant="primary" className="m-0" disabled={isSaving} onClick={uploadImageHandler}>
                                Upload
                            </Button>
                        </Col>
                    </Row>
                    {errors.imageUrl && <p className="text-danger">{errors.imageUrl}</p>}
                </div>

                {/* Визуализиране на снимките */}
                <Row className={styles["upload-box"]}>
                    {photoUrls.map(({ id, url }, index) => (
                        <Col key={id} md={4} className="mb-3">
                            <div className={styles["photo-preview"]}>
                                {index === 0 && <div className={styles["main-photo-label"]}>Main photo</div>}

                                {/* Самата снимка */}
                                <div className={styles["photo-thumbnail"]}>
                                    <Image
                                        src={url}
                                        alt={`Property photo ${index + 1}`}
                                    />
                                    {/* Примерен "X" бутон за затваряне (не е задължителен) */}
                                    <Button
                                        variant="light"
                                        size="sm"
                                        className={styles["close-button"]}
                                        disabled={isSaving}
                                        onClick={() => removeImageHandler(index)}
                                    >
                                        X
                                    </Button>
                                </div>
                            </div>
                            {errors?.ImageUrls && errors.ImageUrls[index]?.map((message, index) => <div key={index} className="text-danger">{message}</div>)}
                        </Col>
                    ))}
                </Row>

                {errors.photoUrls
                    ? <p className="text-danger text-center">{errors.photoUrls.message}</p>
                    : (errors?.ImageUrlsCount && errors.ImageUrlsCount.map((message, index) => <div key={index} className="text-danger text-center">{message}</div>))
                }

                <div className="d-flex justify-content-between mt-3">
                    <Button type="button" className="me-2" disabled={isSaving} onClick={previousStepHandler}>Back</Button>
                    <Button type="submit" disabled={isSaving}>
                        {isSaving
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
                        }</Button>
                </div>
            </Col>
        </Row>
    );
};
