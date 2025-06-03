import ContentLoader, { List } from 'react-content-loader';
import styles from './Loaders.module.css';

export function WheelchairTireSpinner({ style }) {
    return (
        <>
            <div className={styles["wheelchair-tire-spinner"]} style={style}>
                <img src="/img/Wheelchair tire spinner.png" alt="Wheelchair tire spinner" />
            </div>
        </>
    )
};

export function PropertyTypeFilterLoader() {
    return <ContentLoader
        speed={2}
        width={265}
        height={120}
        viewBox="0 0 265 120"
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="220" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="100" height="18" />
        
        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="52" height="18" />
        
        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="112" height="18" />
        
        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="46" height="18" />
        </ContentLoader>
};

export function FacilitiesFilterLoader() {
    return <ContentLoader
        speed={2}
        width={265}
        height={272}
        viewBox="0 0 265 272"
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="120" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="74" height="18" />
        
        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="104" height="18" />
        
        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="111" height="18" />
        
        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="127" height="18" />
        
        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="113" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="115" height="18" />
        
        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="171" height="18" />
        
        <rect x="10" y="203" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="203" rx="0" ry="0" width="64" height="18" />
        
        <rect x="10" y="228" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="228" rx="0" ry="0" width="92" height="18" />
        
        <rect x="10" y="253" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="253" rx="0" ry="0" width="200" height="18" />
        </ContentLoader>
};

export function AccessibilityFilterLoader() {
    return <ContentLoader
        speed={2}
        width={265}
        height={220}
        viewBox="0 0 265 220"
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="143" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="167" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="186" height="18" />
        
        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="202" height="18" />
        
        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="264" height="18" />
        
        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="151" height="18" />
        
        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="173" height="18" />
        
        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="240" height="18" />
        
        <rect x="10" y="203" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="203" rx="0" ry="0" width="154" height="18" />
        </ContentLoader>
};