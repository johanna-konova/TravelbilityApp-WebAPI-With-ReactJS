import { useBasicGetFetch } from "../../../hooks/use-basic-get-fetch";
import { getAccessibility } from "../../../services/facilitiesService";

import SingleAccessibilityConteiner from "./Single-Accessibility-Container";
import { WheelchairTireSpinner } from "../../loaders/Loaders";

import styles from "../Home.module.css";

export default function AccessibilityConteiner() {
    const {
        data: accessibilityData,
        isDataLoaded: isAccessibilityDataLoaded } = useBasicGetFetch(() => getAccessibility());

    return (
        <>
            <section className="container-fluid py-3">
                <div className="container pt-5 pb-3">
                    <div className="text-center mb-2">
                        <span className={styles["section-title"]}>
                            Property Accessibility
                        </span>
                        <span className={styles["section-text"]}>
                            Find a property according to your needs
                        </span>
                    </div>
                    <div className="row justify-content-center">
                        {isAccessibilityDataLoaded
                            ? accessibilityData.map(ad => <SingleAccessibilityConteiner key={ad.id} {...ad} />)
                            : <WheelchairTireSpinner style={{ minHeight: "calc(100vh - 450px)" }}/>
                        }
                    </div>
                </div>
            </section>
        </>
    )
};
