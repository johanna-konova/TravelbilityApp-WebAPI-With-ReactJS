import { useEffect, useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Form, Button, Spinner } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';

import { loginSchema } from '../../validations';
import { useLogin } from '../../hooks/use-auth';

import styles from './Auth-Forms.module.css';

export default function LoginForm() {
    const navigate = useNavigate();
    const location = useLocation();
    const { loginHandler } = useLogin();
    const [errorMessages, setErrorMessages] = useState([]);

    const { register, handleSubmit, formState: { errors, isSubmitting }, reset } = useForm({
        resolver: yupResolver(loginSchema),
    });

    useEffect(() => {
        setErrorMessages([]);
        reset();
    }, [location.key, reset]);

    async function login(userData) {
        try {
            await loginHandler(userData);
            navigate("/");
        } catch (error) {
            setErrorMessages(error.messages)
        }
    }

    return (
        <>
            <Container className="text-center mt-3">
                <h2 className="text-primary">
                    Log <span className="text-dark">in</span>
                </h2>
                <Row className={styles["auth-container"]}>
                    <Col lg={7}>
                        <Card className="border-0">
                            <Card.Header className={`${styles["auth-card-header"]} card-header p-5`} />
                            <Card.Body className="rounded-bottom bg-white p-5">
                                <Form className={styles["auth-form"]} onSubmit={handleSubmit(login)}>

                                    {errorMessages && (
                                        <div className="mb-2">
                                            {errorMessages.map((em, i) => (
                                                <p key={i} className={styles["global-error-message"]}>
                                                    {em}
                                                </p>
                                            ))}
                                        </div>
                                    )}

                                    <Form.Group className="mb-3">
                                        <Form.Label>Email</Form.Label>
                                        <Form.Control
                                            type="email"
                                            placeholder="Ex.: john.doe@mail.com"
                                            disabled={isSubmitting}
                                            {...register('email')}
                                        />
                                        {errors.email && <p className="text-danger">{errors.email.message}</p>}
                                    </Form.Group>

                                    <Form.Group className="mb-3">
                                        <Form.Label>Password</Form.Label>
                                        <Form.Control
                                            type="password"
                                            placeholder="********"
                                            disabled={isSubmitting}
                                            {...register('password')}
                                        />
                                        {errors.password && <p className="text-danger">{errors.password.message}</p>}
                                    </Form.Group>

                                    <Button variant="primary" type="submit" className={`${styles["submit-text"]} btn-block py-3`} disabled={isSubmitting}>
                                        {isSubmitting
                                            ? <>
                                                <Spinner
                                                    as="span"
                                                    animation="border"
                                                    size="sm"
                                                    role="status"
                                                    aria-hidden="true"
                                                />
                                                Logging in...
                                            </>
                                            : <span>Log in</span>
                                        }</Button>
                                </Form>
                                {!isSubmitting &&
                                    <p className="text-center mt-3">
                                        New in Travelbility? <Link to="/register">Sign up now</Link>
                                    </p>
                                }
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </>
    )
};
