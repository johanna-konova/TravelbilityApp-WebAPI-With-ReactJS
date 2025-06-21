import { useEffect, useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Container, Row, Col, Card, Form, Button, Spinner } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';

import { registrationSchema } from '../../validations';
import { useRegister } from '../../hooks/use-auth';

import styles from './Auth-Forms.module.css';

export default function RegisterForm() {
    const navigate = useNavigate();
    const location = useLocation();
    const { registerHandler } = useRegister();
    const [menualErrors, setMenualErrors] = useState({});

    const { register: registerInput, handleSubmit, formState: { errors, isSubmitting }, reset } = useForm({
        resolver: yupResolver(registrationSchema),
    });

    useEffect(() => {
        setMenualErrors({});
        reset();
    }, [location.key, setMenualErrors, reset]);

    async function register(userData) {
        try {
            await registerHandler(userData);
            navigate("/");
        } catch (error) {
            setMenualErrors(error.errorsData)
        }
    }

    return (
        <>
            <Container className="text-center mt-3">
                <h2 className="text-primary">
                    Sign <span className="text-dark">up</span>
                </h2>
                <Row className={styles["auth-container"]}>
                    <Col lg={7}>
                        <Card className="border-0">
                            <Card.Header className={`${styles["auth-card-header"]} card-header p-5`} />
                            <Card.Body className="rounded-bottom bg-white p-5">
                                <Form className={styles["auth-form"]} onSubmit={handleSubmit(register)}>
                                    <Form.Group className="mb-3">
                                        <Form.Label>Email</Form.Label>
                                        <Form.Control
                                            type="text"
                                            placeholder="Ex.: john.doe@mail.com"
                                            disabled={isSubmitting}
                                            {...registerInput('email')}
                                        />
                                        {errors.email
                                            ? <p className="text-danger">{errors.email.message}</p>
                                            : (menualErrors.Email && menualErrors.Email.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                                        }
                                    </Form.Group>

                                    <Form.Group className="mb-3">
                                        <Form.Label>Password</Form.Label>
                                        <Form.Control
                                            type="password"
                                            placeholder="********"
                                            disabled={isSubmitting}
                                            {...registerInput('password')}
                                        />
                                        {errors.password
                                            ? <p className="text-danger">{errors.password.message}</p>
                                            : (menualErrors.Password && menualErrors.Password.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                                        }
                                    </Form.Group>

                                    <Form.Group className="mb-3">
                                        <Form.Label>Confirm Password</Form.Label>
                                        <Form.Control
                                            type="password"
                                            placeholder="********"
                                            disabled={isSubmitting}
                                            {...registerInput('confirmedPassword')}
                                        />
                                        {errors.confirmedPassword
                                            ? <p className="text-danger">{errors.confirmedPassword.message}</p>
                                            : (menualErrors.ConfirmedPassword && menualErrors.ConfirmedPassword.map((message, index) => <div key={index} className="text-danger">{message}</div>))
                                        }
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
                                                Signing up...
                                            </>
                                            : <span>Sign up</span>
                                        }
                                    </Button>
                                </Form>
                                {!isSubmitting &&
                                    <p className="text-center mt-3">
                                        Already a member? <Link to="/login">Log in</Link>
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
