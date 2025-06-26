import { Link } from "react-router-dom";

import { useAuthContext } from "../../../contexts/Auth-Context";
import { PropertiesContext } from "../../../contexts/Properties-Context";

import { useBasicGetFetch } from "../../../hooks/use-basic-get-fetch";
import { getNewestAdded } from "../../../services/propertiesService";

import SingeNewestAddedPropertyContainer from "./Single-Neweset-Added-Property-Container";
import { WheelchairTireSpinner } from "../../loaders/Loaders";

import styles from "../Home.module.css";

export default function NewestAddedPropertiesContainer() {
    const { id } = useAuthContext();
    const {
        data: newestAddedPropertiesData,
        isDataLoaded: areNewestAddedPropertiesDataLoaded,
        removeDataElement: deletePropertyByIdHandler } = useBasicGetFetch(() => getNewestAdded(3));

    return (
        <>
            <div className="container-fluid py-3">
                <div className="container pt-5 pb-3">
                    <div className="text-center mb-3">
                        <span className={styles["section-title"]}>
                            Properties
                        </span>
                        <span className={styles["section-text"]}>
                            Explore the newest added
                        </span>
                    </div>
                    <div className="row justify-content-center">
                        {areNewestAddedPropertiesDataLoaded
                            ? <PropertiesContext.Provider value={{ deletePropertyByIdHandler }}>
                                {newestAddedPropertiesData.map(napd =>
                                    <SingeNewestAddedPropertyContainer
                                        key={napd.id}
                                        {...napd}
                                        isLoggedInUserPropertyDataPublisher={id === napd.publisherId}
                                    />
                                )}
                            </PropertiesContext.Provider>
                            : <WheelchairTireSpinner style={{ minHeight: "calc(100vh - 450px)" }} />
                        }
                    </div>
                    <div className={styles["explore-all"]}>
                        <Link to={`/properties`}>Explore all properties</Link>
                    </div>
                </div>
            </div>
        </>
    )
};
