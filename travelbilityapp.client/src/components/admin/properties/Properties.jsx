import { useState } from "react";
import { Button, Col, Container, Row, Spinner } from "react-bootstrap";
import toast from "react-hot-toast";

import { useBasicGetFetch } from "../../../hooks/use-basic-get-fetch";
import { getAllForAdmin, publish, reject } from "../../../services/propertiesService";
import { getCountryName } from "../../../utils/property-utils";

import { WheelchairTireSpinner } from "../../loaders/Loaders";

import styles from "./Properties.module.css";

export default function Properties() {
    const {
        data: propertiesData,
        isDataLoaded: arePropertiesDataLoaded,
        updateData: updatePropertiesDataHandler, } = useBasicGetFetch(getAllForAdmin);

    const [isPending, setIsPending] = useState(false);

    const approveHandler = async (id, name) =>
        updateStatusHandler("Published", id, name);

    const rejectHandler = async (id, name) =>
        updateStatusHandler("Rejected", id, name);

    const updateStatusHandler = async (status, id, name) => {
        setIsPending(true);

        status === "Published"
            ? await publish(id)
            : await reject(id);

        updatePropertiesDataHandler(previousPropertiesData =>
            previousPropertiesData.map(ppd => id === ppd.id ? { ...ppd, status } : ppd
        ));

        toast.success(`You have successfully ${status === "Published" ? "approved" : "rejected"} the ${name}.`, { position: "buttom-right" });
        setIsPending(false);
    };

    return (
        <Container className="ml-3">
            <p className={styles["properties-label"]}>Properties</p>

            {arePropertiesDataLoaded
                ? <>
                    <Row className={styles["properties-list"]}>
                        <Col sm={2} md={2} lg={2}>
                            <span>Name</span>
                        </Col>
                        <Col sm={2} md={2} lg={2}>
                            <span>Type</span>
                        </Col>
                        <Col sm={2} md={2} lg={2}>
                            <span>Destination</span>
                        </Col>
                        <Col sm={2} md={2} lg={2}>
                            <span>Publisher</span>
                        </Col>
                        <Col sm={2} md={2} lg={2}>
                            <span>Status</span>
                        </Col>
                        <Col sm={2} md={2} lg={2}>
                            <span>Actions</span>
                        </Col>
                    </Row>

                    {propertiesData.map((pd, i) =>
                        <Row key={i} className={styles["properties-list"]}>
                            <Col sm={2} md={2} lg={2}>
                                <span>{pd.name}</span>
                            </Col>
                            <Col sm={2} md={2} lg={2}>
                                <span>{pd.typeName}</span>
                            </Col>
                            <Col sm={2} md={2} lg={2}>
                                <span>{getCountryName(pd.address)}</span>
                            </Col>
                            <Col sm={2} md={2} lg={2}>
                                <span>{pd.publisher}</span>
                            </Col>
                            <Col sm={2} md={2} lg={2}>
                                <span>{pd.status}</span>
                            </Col>
                            <Col sm={2} md={2} lg={2} className="actions">
                                <Button type="button" variant="primary" size="sm" disabled={pd.status !== "Pending" || isPending} onClick={() => approveHandler(pd.id, pd.name)}>
                                    {isPending
                                        ? <>
                                            <Spinner
                                                as="span"
                                                animation="border"
                                                size="sm"
                                                role="status"
                                                aria-hidden="true"
                                            />
                                            <i className="fas fa-check"></i> Approving...
                                        </>
                                        : <span><i className="fas fa-check"></i> Approve</span>
                                    }
                                </Button>

                                <Button type="button" variant="danger" size="sm" disabled={pd.status !== "Pending" || isPending} onClick={() => rejectHandler(pd.id, pd.name)}>
                                    {isPending
                                        ? <>
                                            <Spinner
                                                as="span"
                                                animation="border"
                                                size="sm"
                                                role="status"
                                                aria-hidden="true"
                                            />
                                            <i className="fas fa-check"></i> Rejecting...
                                        </>
                                        : <span><span className={styles["x"]}>X</span> Reject</span>
                                    }
                                </Button>
                            </Col>
                        </Row>
                    )}
                </>
                : <WheelchairTireSpinner style={{ minHeight: "calc(125vh - 450px)" }} />
            }
        </Container>
    );
}