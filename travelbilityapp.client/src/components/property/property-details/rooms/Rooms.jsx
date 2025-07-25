import { useMemo } from 'react';

import { useBasicGetFetch } from '../../../../hooks/use-basic-get-fetch';
import { getAllDetailed } from '../../../../services/roomsService';

import RoomContainer from './Room-Container';

export default function Rooms({ propertyId }) {
    const {
        data: roomsData,
        isDataLoaded: isRoomsDataLoaded
    } = useBasicGetFetch(() => getAllDetailed(propertyId));

    const { accessibleRooms, nonAccessibleRooms } = useMemo(() => {
        return (roomsData || []).reduce((acc, rd) => {
            rd.isAccessibleRoom
                ? acc.accessibleRooms.push(rd)
                : acc.nonAccessibleRooms.push(rd);

            return acc;
        }, { accessibleRooms: [], nonAccessibleRooms: [] });
    }, [roomsData]);

    return (
        <>
            <RoomContainer
                areAccessibleRooms={true}
                roomsData={accessibleRooms}
                propertyId={propertyId}
            />

            <RoomContainer
                areAccessibleRooms={false}
                roomsData={nonAccessibleRooms}
                propertyId={propertyId}
            />
        </>
    );
};