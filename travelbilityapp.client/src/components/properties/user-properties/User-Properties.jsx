import { Container } from 'react-bootstrap';

import { useAuthContext } from '../../../contexts/Auth-Context';
import { PropertiesContext } from '../../../contexts/Properties-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getAllByPublisherId } from '../../../services/propertiesService';

import NoProperties from './No-Properties';
import UserPropertiesContainer from './User-Properties-Container';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

export default function UserProperties() {
    const { id } = useAuthContext();
    const {
        data: propertiesDataByPublisherId,
        isDataLoaded: isPropertiesDataByPublisherIdLoaded,
        removeDataElement: deletePropertyByIdHandler } = useBasicGetFetch(() => getAllByPublisherId(id));

    return (
        <>
            <Container className="mt-5">
                {isPropertiesDataByPublisherIdLoaded
                    ? propertiesDataByPublisherId.length === 0
                        ? <NoProperties />
                        : <PropertiesContext.Provider value={{ deletePropertyByIdHandler }}>
                            <UserPropertiesContainer propertiesDataByPublisherId={propertiesDataByPublisherId} />
                          </PropertiesContext.Provider>
                    : <WheelchairTireSpinner />
                }
            </Container>
        </>
    )
};
