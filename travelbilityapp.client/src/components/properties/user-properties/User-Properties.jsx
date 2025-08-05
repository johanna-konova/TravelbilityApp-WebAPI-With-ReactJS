import { Container } from 'react-bootstrap';

import { useAuthContext } from '../../../contexts/Auth-Context';
import { PropertiesContext } from '../../../contexts/Properties-Context';

import { usePageParams } from '../../../hooks/use-page-params';
import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getAllByPublisherId } from '../../../services/propertiesService';

import NoProperties from './No-Properties';
import UserPropertiesContainer from './User-Properties-Container';
import Paginator from '../all-properties/Paginator';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

export default function UserProperties() {
    const { id } = useAuthContext();
    const [currentPageNumber, updatecurrentPageNumber] = usePageParams();

    const {
        data: pagedResult,
        isDataLoaded: isPagedResultLoaded,
        removeDataElement: deletePropertyByIdHandler } = useBasicGetFetch(() => getAllByPublisherId(currentPageNumber), {}, [currentPageNumber]);

    return (
        <>
            <Container className="mt-5">
                {isPagedResultLoaded
                    ? pagedResult.items.length === 0
                        ? <NoProperties />
                        : <>
                            <PropertiesContext.Provider value={{ deletePropertyByIdHandler }}>
                                <UserPropertiesContainer propertiesDataByPublisherId={pagedResult.items} />
                            </PropertiesContext.Provider>

                            <div className="d-flex justify-content-center">
                                <Paginator
                                    currentPage={currentPageNumber}
                                    totalCount={pagedResult.totalCount}
                                    itemsPerPage={pagedResult.itemsPerPage}
                                    updatecurrentPageNumber={updatecurrentPageNumber}
                                />
                            </div>
                        </>
                    : <WheelchairTireSpinner />
                }
            </Container>
        </>
    )
};
