import { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { Form, Button, Container, Row, Col, Alert } from 'react-bootstrap';
import Error from "./Error.jsx";
import {createOrder} from "../OrderApiHelper.jsx";

const CreateOrder = () => {
  const navigate = useNavigate();
  const [error, setError] = useState();
  const [formData, setFormData] = useState({
    senderCity: '',
    senderAddress: '',
    receiverCity: '',
    receiverAddress: '',
    weight: '',
    pickupDate: ''
  });

  // Состояние для отображения ошибок валидации
  const [errors, setErrors] = useState({});
  // Состояние успешной отправки (для демонстрации)
  const [submitted, setSubmitted] = useState(false);

  // Обработчик изменения любого поля
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    // Удаляем ошибку для этого поля, если пользователь начал исправлять
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: '' }));
    }
  };

  // Функция валидации: возвращает объект с ошибками
  const validate = () => {
    const newErrors = {};
    if (!formData.senderCity.trim()) newErrors.senderCity = 'Укажите город отправителя';
    if (!formData.senderAddress.trim()) newErrors.senderAddress = 'Укажите адрес отправителя';
    if (!formData.receiverCity.trim()) newErrors.receiverCity = 'Укажите город получателя';
    if (!formData.receiverAddress.trim()) newErrors.receiverAddress = 'Укажите адрес получателя';
    if (!formData.weight.trim()) newErrors.weight = 'Укажите вес груза';
    else if (isNaN(formData.weight) || Number(formData.weight) <= 0) {
      newErrors.weight = 'Вес должен быть положительным числом';
    }
    if (!formData.pickupDate) newErrors.pickupDate = 'Выберите дату забора груза';
    return newErrors;
  };

  // Обработчик отправки формы
  const handleSubmit = async (e) => {
    e.preventDefault();
    const validationErrors = validate();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      setSubmitted(false);
    } else {
      setErrors({});
      // Созранение на сервер
      await createOrder(formData, setError, setSubmitted);
      // Сбросить сообщение об успехе через 5 секунд
      setTimeout(() => setSubmitted(false), 5000);
    }
  };

  return (
    <Container className="mt-4">
      <Row className="justify-content-md-center">
        <Col md={8}>
          <h2 className="mb-4">Оформление заказа</h2>
          {submitted && (
            <Alert variant="success" onClose={() => setSubmitted(false)} dismissible>
              Заказ успешно создан!
            </Alert>
          )}
          <Form noValidate onSubmit={handleSubmit}>
            {/* Город отправителя */}
            <Form.Group className="mb-3" controlId="senderCity">
              <Form.Label>Город отправителя *</Form.Label>
              <Form.Control
                type="text"
                name="senderCity"
                value={formData.senderCity}
                onChange={handleChange}
                isInvalid={!!errors.senderCity}
                placeholder="Например, Москва"
              />
              <Form.Control.Feedback type="invalid">
                {errors.senderCity}
              </Form.Control.Feedback>
            </Form.Group>

            {/* Адрес отправителя */}
            <Form.Group className="mb-3" controlId="senderAddress">
              <Form.Label>Адрес отправителя *</Form.Label>
              <Form.Control
                type="text"
                name="senderAddress"
                value={formData.senderAddress}
                onChange={handleChange}
                isInvalid={!!errors.senderAddress}
                placeholder="Улица, дом, офис"
              />
              <Form.Control.Feedback type="invalid">
                {errors.senderAddress}
              </Form.Control.Feedback>
            </Form.Group>

            {/* Город получателя */}
            <Form.Group className="mb-3" controlId="receiverCity">
              <Form.Label>Город получателя *</Form.Label>
              <Form.Control
                type="text"
                name="receiverCity"
                value={formData.receiverCity}
                onChange={handleChange}
                isInvalid={!!errors.receiverCity}
                placeholder="Например, Санкт-Петербург"
              />
              <Form.Control.Feedback type="invalid">
                {errors.receiverCity}
              </Form.Control.Feedback>
            </Form.Group>

            {/* Адрес получателя */}
            <Form.Group className="mb-3" controlId="receiverAddress">
              <Form.Label>Адрес получателя *</Form.Label>
              <Form.Control
                type="text"
                name="receiverAddress"
                value={formData.receiverAddress}
                onChange={handleChange}
                isInvalid={!!errors.receiverAddress}
                placeholder="Улица, дом, офис"
              />
              <Form.Control.Feedback type="invalid">
                {errors.receiverAddress}
              </Form.Control.Feedback>
            </Form.Group>

            {/* Вес груза */}
            <Form.Group className="mb-3" controlId="weight">
              <Form.Label>Вес груза (кг) *</Form.Label>
              <Form.Control
                type="number"
                name="weight"
                value={formData.weight}
                onChange={handleChange}
                isInvalid={!!errors.weight}
                placeholder="0.0"
                step="0.1"
              />
              <Form.Control.Feedback type="invalid">
                {errors.weight}
              </Form.Control.Feedback>
            </Form.Group>

            {/* Дата забора груза */}
            <Form.Group className="mb-4" controlId="pickupDate">
              <Form.Label>Дата забора груза *</Form.Label>
              <Form.Control
                type="date"
                name="pickupDate"
                value={formData.pickupDate}
                onChange={handleChange}
                isInvalid={!!errors.pickupDate}
              />
              <Form.Control.Feedback type="invalid">
                {errors.pickupDate}
              </Form.Control.Feedback>
            </Form.Group>

            {error && <Error message={error.message}/>}

            <Button variant="primary" type="submit">
              Создать заказ
            </Button>

            <Button variant='secondary' style={{marginLeft:"10px"}} onClick={() => {navigate('/');}}>Назад</Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
}

export default CreateOrder;