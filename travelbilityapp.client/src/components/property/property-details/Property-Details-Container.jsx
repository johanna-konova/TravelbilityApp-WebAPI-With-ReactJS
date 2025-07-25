import { Container } from 'react-bootstrap';

import { usePropertyContext } from '../../../contexts/Property-Context';
import { useAuthContext } from '../../../contexts/Auth-Context';

import PropertyDetailsImages from './Property-Details-Images';
import UserActions from '../../user-actions/User-Actions';
import PropertyDetails from './Property-Details';
import Rooms from './rooms/Rooms';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

export default function PropertyDetailsContainer() {
    const { propertyData, isPropertyDataLoaded } = usePropertyContext();
    const { id } = useAuthContext();

    return (
        <>
            <Container className="mt-5">
                {isPropertyDataLoaded
                    ? <>
                        {propertyData.photoUrls && <PropertyDetailsImages photoUrls={propertyData.photoUrls} />}

                        {(id === propertyData.publisherId) && <UserActions id={propertyData.id} name={propertyData.name} />}

                        <PropertyDetails {...propertyData} />

                        <Rooms propertyId={propertyData.id} />
                    </>
                    : <WheelchairTireSpinner />
                }

            </Container>
        </>
    )
};
