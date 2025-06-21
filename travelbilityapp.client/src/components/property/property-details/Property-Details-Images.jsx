import React from 'react';
import { Container } from 'react-bootstrap';

import styles from './Property-Details.module.css';

export default function PropertyDetailsImages({ imageUrls }) {
    return (
        <Container className={styles["d-flex justify-content-center"]}>
            <div className={styles["gallery-container"]}>
                <div className={styles["main-image"]}>
                    <img src={imageUrls[0]} alt="Main view" />
                </div>
                <div className={styles["thumbnail-container"]}>
                    <div className={styles["thumbnail"]}>
                        <img src={imageUrls[1]} alt="Thumbnail 1" />
                    </div>
                    <div className={styles["thumbnail"]}>
                        <img src={imageUrls[2]} alt="Thumbnail 2" />
                    </div>
                    <div className={styles["thumbnail"]}>
                        <img src={imageUrls[3]} alt="Thumbnail 1" />
                    </div>
                    <div className={styles["thumbnail"]}>
                        <img src={imageUrls[4]} alt="Thumbnail 2" />
                    </div>
                </div>
            </div>
        </Container>
    )
};
