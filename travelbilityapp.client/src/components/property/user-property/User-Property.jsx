import { Container } from "react-bootstrap";

import { useUserPropertyContext } from "../../../contexts/User-Property-Context";

import UserPropertyDetails from "./User-Property-Details";
import UserActions from "../../user-actions/User-Actions";
import Rooms from "./rooms/Rooms";
import { WheelchairTireSpinner } from "../../loaders/Loaders";

export default function UserProperty() {
    const { propertyData, isPropertyDataLoaded } = useUserPropertyContext();

    return (
        <Container className="mt-5">
            {isPropertyDataLoaded
                ? <>
                    <Rooms propertyId={propertyData.id} inputPropertyStatus={propertyData.status} />

                    <UserPropertyDetails propertyData={propertyData} />

                    <UserActions
                        id={propertyData.id}
                        name={propertyData.name}
                        hasPaddingBottom={true}
                    />
                </>
                : <WheelchairTireSpinner />
            }
        </Container>
    );
};