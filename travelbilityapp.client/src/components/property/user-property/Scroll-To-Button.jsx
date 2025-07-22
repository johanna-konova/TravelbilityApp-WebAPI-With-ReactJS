import { useEffect, useState } from "react";

import styles from './User-Property.module.css';

export default function ScrollToButton({ propertyName, targetRef }) {
    const [isVisible, setIsVisible] = useState(true);

    useEffect(() => {
        const handleScroll = () => {
            if (!targetRef?.current) return;

            const rect = targetRef.current.getBoundingClientRect();

            // Ако елементът е видим в прозореца
            if (rect.top <= (window.innerHeight * 0.5) && rect.bottom >= 0) {
                setIsVisible(false);
            } else {
                setIsVisible(true);
            }
        };

        window.addEventListener("scroll", handleScroll);
        handleScroll(); // Първоначална проверка
        return () => window.removeEventListener("scroll", handleScroll);
    }, [targetRef]);

    const scrollToSection = () => {
        if (targetRef?.current) {
            targetRef.current.scrollIntoView({ behavior: "smooth" });
        }
    };

    if (!isVisible) return null;

    return (
        <button
            className={`${styles["scroll-to-button"]} btn btn-lg btn-primary`}
            onClick={scrollToSection}
        >
            Go to the {propertyName}'s details <i className="fa fa-angle-double-down" />
        </button>
    );
}
