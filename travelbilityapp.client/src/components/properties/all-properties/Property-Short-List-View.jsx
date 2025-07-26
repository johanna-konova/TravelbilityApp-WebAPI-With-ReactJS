import { Link } from "react-router-dom";
import { Card } from "react-bootstrap";

import { generateStarIcons } from "../../../utils/property-utils";

import UserActions from "../../user-actions/User-Actions";

import styles from './All-Properties.module.css';

export default function PropertyShortListView({
    id,
    mainPhotoUrl,
    name,
    starsCount,
    address,
    accessibilityNames,
    isLoggedInUserPropertyDataCreator
}) {
    return (
        <div className={styles["property-container"]}>
            <div className={styles["property-img-container"]}>
                <Link to={`/properties/${id}`}>
                    <img src={mainPhotoUrl} alt={`${name}'s main photo`} />
                </Link>
            </div>
            <div className={styles["property-info-container"]}>
                <Card.Title className="m-0">
                    <Link to={`/properties/${id}`}>{name}</Link>
                </Card.Title>
                <div className={styles["stars"]}>{generateStarIcons(Number(starsCount || 0))}</div>
                <div className="mt-2">
                    <i className="fas fa-map-marker-alt"></i> <span className={styles["address"]}>{address}</span>
                </div>
                <div className={styles["accessibility-container"]}>
                    <i className="fab fa-accessible-icon text-primary"></i> <span className="text-primary">Property accessibility:</span>
                    <div className="ml-3">
                        {accessibilityNames?.map((an, i) =>
                            <div key={i}>
                                <i className="fas fa-check text-primary"></i> <span>{an};</span>
                            </div>
                        )}
                    </div>
                </div>

                {isLoggedInUserPropertyDataCreator && <UserActions id={id} name={name} />}
            </div>
        </div>
    )
};
