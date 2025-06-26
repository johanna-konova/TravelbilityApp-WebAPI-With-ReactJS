import { Container, Row } from "react-bootstrap";

import PropertyShortGridView from "./Property-Short-Grid-View";

export default function UserPropertiesContainer({ propertiesDataByPublisherId }) {
    return (
        <>
            <Container>
                <Row className="justify-content-center">
                    {propertiesDataByPublisherId.map(pdbpi =>
                        <PropertyShortGridView
                            key={pdbpi.id}
                            propertyData={pdbpi}
                        />)
                    }
                </Row>
            </Container>
        </>
    )
};
