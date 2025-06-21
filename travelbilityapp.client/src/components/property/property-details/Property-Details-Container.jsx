import { Container } from 'react-bootstrap';

import { usePropertyContext } from '../../../contexts/Property-Context';
import { useAuthContext } from '../../../contexts/Auth-Context';

import PropertyDetailsImages from './Property-Details-Images';
import UserActions from '../../user-actions/User-Actions';
import PropertyDetails from './Property-Details';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

export default function PropertyDetailsContainer() {
    const { propertyData, isPropertyDataLoaded } = usePropertyContext();
    const { id } = useAuthContext();
    console.log(propertyData.publisherId);
    console.log(id);

    return (
        <>
            <Container className="mt-5">
                {isPropertyDataLoaded
                    ? <>
                        {propertyData.imageUrls && <PropertyDetailsImages imageUrls={propertyData.imageUrls} />}

                        {(id === propertyData.publisherId) && <UserActions id={propertyData.id} name={propertyData.name} />}

                        <PropertyDetails {...propertyData} />
                    </>
                    : <WheelchairTireSpinner />
                }

            </Container>
        </>
    )
};
