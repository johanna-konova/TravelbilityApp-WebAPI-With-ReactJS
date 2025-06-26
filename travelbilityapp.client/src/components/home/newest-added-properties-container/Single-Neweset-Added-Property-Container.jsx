import { Link } from "react-router-dom";

import UserActions from "../../user-actions/User-Actions";

import { getCountryName } from "../../../utils/property-utils";

import styles from "../Home.module.css";

export default function SingeNewestAddedPropertyContainer({
    id,
    name,
    typeName,
    starsCount,
    address,
    mainPhotoUrl,
    isLoggedInUserPropertyDataPublisher
}) {
    const countryName = getCountryName(address);

    return (
        <div className="col-lg-4 col-md-6">
            <div className={styles["newest"]}>
                <Link to={`/properties/${id}`}>
                    <img className="img-fluid" src={mainPhotoUrl} alt={`${name}'s main photo`} />
                </Link>
                <div className={styles["property-info"]}>
                    <div className="pt-4 pl-4 pr-4 pb-0">
                        <div className="d-flex justify-content-between mb-3">
                            <small className="col-5 m-0 p-1">
                                <i className="fas fa-hotel text-primary mr-1" />
                                <span title={typeName}>
                                    {`${typeName.slice(0, 8)}${typeName.length > 8 ? "..." : ""}`}
                                </span>
                            </small>
                            <small className="col-2 m-0 p-1">
                                <i className="fas fa-star text-primary mr-1" />
                                {starsCount}
                            </small>
                            <small className="col-5 m-0 p-1 text-center">
                                <i className="fas fa-map-marker-alt text-primary mr-1" />
                                <span title={countryName}>
                                    {`${countryName.slice(0, 8)}${countryName.length > 8 ? "..." : ""}`}
                                </span>
                            </small>
                        </div>
                        <div className={styles["name"]}>
                            <Link to={`/properties/${id}`} className="h5 text-decoration-none">{name}</Link>
                        </div>
                    </div>

                    {isLoggedInUserPropertyDataPublisher && <UserActions id={id} name={name} hasPaddingBottom={true} />}
                </div>
            </div>
        </div>
    )
};
