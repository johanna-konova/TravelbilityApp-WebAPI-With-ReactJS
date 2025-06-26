import { Link } from "react-router-dom";
import { Card } from "react-bootstrap";
import { List } from "react-content-loader";

import { useBasicGetFetch } from "../../../hooks/use-basic-get-fetch";
import { generateStarIcons } from "../../../utils/property-utils";

import UserActions from "../../user-actions/User-Actions";

import styles from './All-Properties.module.css';

export default function PropertyShortListView({
    id,
    mainPhoto,
    name,
    starsCount,
    address,
    accessibility,
    isLoggedInUserPropertyDataCreator
}) {
    /*const {
        data: propertyAccessibility,
        isDataLoaded: isPropertyAccessibilityLoaded } = useBasicGetFetch(() => getPropertyAccessibilityById(id));*/

    return (
        <div className={styles["property-container"]}>
            <div className={styles["property-img-container"]}>
                <Link to={`/properties/${id}`}>
                    <img src={mainPhoto} alt={`${name}'s main photo`} />
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
                    <i className="fab fa-accessible-icon text-primary"></i> <span className="text-primary">Accessibility:</span>
                    <div className="ml-3">
                        {accessibility?.map((a, i) =>
                            <div key={i}>
                                <i className="fas fa-check text-primary"></i> <span>{a};</span>
                            </div>
                            )
                        }
                    </div>
                </div>

                {isLoggedInUserPropertyDataCreator && <UserActions id={id} name={name} />}
            </div>
        </div>
    )
};
