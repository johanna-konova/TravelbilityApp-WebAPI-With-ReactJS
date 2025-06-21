import { Container } from 'react-bootstrap';

import { useAuthContext } from '../../../contexts/Auth-Context';
import { PropertiesContext } from '../../../contexts/Properties-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getByPublisherId } from '../../../services/propertiesService';

import NoProperties from './No-Properties';
import UserPropertiesContainer from './User-Properties-Container';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

export default function UserProperties() {
    const { id } = useAuthContext();
    const {
        data: propertiesDataByOwnerId,
        isDataLoaded: isPropertiesDataByOwnerIdLoaded,
        removeDataElement: deletePropertyByIdHandler } = useBasicGetFetch(() => getByPublisherId(id));

    return (
        <>
            <Container className="mt-5">
                {isPropertiesDataByOwnerIdLoaded
                    ? propertiesDataByOwnerId.length === 0
                        ? <NoProperties />
                        : <PropertiesContext.Provider value={{ deletePropertyByIdHandler }}>
                            <UserPropertiesContainer propertiesDataByOwnerId={propertiesDataByOwnerId} />
                          </PropertiesContext.Provider>
                    : <WheelchairTireSpinner />
                }
            </Container>
        </>
    )
};
