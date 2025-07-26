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

export function RoomTypeFilterLoader() {
    return <ContentLoader
        speed={2}
        width={265}
        height={198}
        viewBox="0 0 265 198"
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="102" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="110" height="18" />

        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="90" height="18" />

        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="108" height="18" />

        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="108" height="18" />

        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="42" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="53" height="18" />

        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="141" height="18" />
    </ContentLoader>
};

export function PropertyFacilitiesFilterLoader() {
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
        <rect x="35" y="28" rx="0" ry="0" width="83" height="18" />
        
        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="104" height="18" />
        
        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="128" height="18" />
        
        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="116" height="18" />
        
        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="172" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="64" height="18" />
        
        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="93" height="18" />
        
        <rect x="10" y="203" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="203" rx="0" ry="0" width="200" height="18" />
        
        <rect x="10" y="228" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="228" rx="0" ry="0" width="104" height="18" />
        </ContentLoader>
};

export function PropertyAccessibilityFilterLoader() {
    return <ContentLoader
        speed={2}
        height={275}
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="144" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="168" height="18" />
        
        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="187" height="18" />
        
        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="202" height="18" />
        
        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="265" height="18" />
        
        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="151" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="173" height="18" />
        
        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="241" height="18" />
        
        <rect x="10" y="203" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="203" rx="0" ry="0" width="154" height="18" />

        <rect x="10" y="228" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="228" rx="0" ry="0" width="265" height="18" />

        <rect x="10" y="253" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="253" rx="0" ry="0" width="157" height="18" />
        </ContentLoader>
};

export function RoomFacilitiesFilterLoader() {
    return <ContentLoader
        speed={2}
        width={265}
        height={574}
        viewBox="0 0 265 574"
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="83" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="104" height="18" />

        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="112" height="18" />

        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="113" height="18" />

        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="172" height="18" />

        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="146" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="73" height="18" />

        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="67" height="18" />

        <rect x="10" y="203" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="203" rx="0" ry="0" width="173" height="18" />

        <rect x="10" y="228" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="228" rx="0" ry="0" width="64" height="18" />

        <rect x="10" y="253" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="253" rx="0" ry="0" width="153" height="18" />

        <rect x="10" y="278" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="278" rx="0" ry="0" width="132" height="18" />

        <rect x="10" y="303" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="303" rx="0" ry="0" width="77" height="18" />

        <rect x="10" y="328" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="328" rx="0" ry="0" width="22" height="18" />

        <rect x="10" y="353" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="353" rx="0" ry="0" width="121" height="18" />

        <rect x="10" y="378" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="378" rx="0" ry="0" width="99" height="18" />

        <rect x="10" y="403" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="403" rx="0" ry="0" width="123" height="18" />

        <rect x="10" y="428" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="428" rx="0" ry="0" width="112" height="18" />

        <rect x="10" y="453" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="453" rx="0" ry="0" width="100" height="18" />

        <rect x="10" y="478" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="478" rx="0" ry="0" width="43" height="18" />

        <rect x="10" y="503" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="503" rx="0" ry="0" width="79" height="18" />

        <rect x="10" y="528" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="528" rx="0" ry="0" width="66" height="18" />

        <rect x="10" y="553" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="553" rx="0" ry="0" width="91" height="18" />
    </ContentLoader>
};

export function RoomAccessibilityFilterLoader() {
    return <ContentLoader
        speed={2}
        height={250}
        backgroundColor="#f3f3f3"
        foregroundColor="#ecebeb"
    >
        <rect x="10" y="3" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="3" rx="0" ry="0" width="144" height="18" />

        <rect x="10" y="28" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="28" rx="0" ry="0" width="168" height="18" />

        <rect x="10" y="53" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="53" rx="0" ry="0" width="187" height="18" />

        <rect x="10" y="78" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="78" rx="0" ry="0" width="202" height="18" />

        <rect x="10" y="103" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="103" rx="0" ry="0" width="151" height="18" />

        <rect x="10" y="128" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="128" rx="0" ry="0" width="173" height="18" />

        <rect x="10" y="153" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="153" rx="0" ry="0" width="241" height="18" />

        <rect x="10" y="178" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="178" rx="0" ry="0" width="154" height="18" />

        <rect x="10" y="203" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="203" rx="0" ry="0" width="119" height="18" />

        <rect x="10" y="228" rx="3" ry="3" width="18" height="18" />
        <rect x="35" y="228" rx="0" ry="0" width="108" height="18" />
    </ContentLoader>
};