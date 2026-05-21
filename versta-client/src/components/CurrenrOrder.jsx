import { useState } from 'react';
import { Form, Container, Row, Col, Card } from 'react-bootstrap';

const CurrentOrder = () => {
  const defaultOrder = {
    orderNumber: 'ЗАКАЗ-001',
    senderCity: 'Москва',
    senderAddress: 'ул. Тверская, д. 1, оф. 5',
    receiverCity: 'Санкт-Петербург',
    receiverAddress: 'Невский пр., д. 10, кв. 12',
    weight: 15.5,
    pickupDate: '2025-03-25'
  };

  const data = defaultOrder;

  return (
    <Container className="mt-4">
      <Row className="justify-content-md-center">
        <Col md={8}>
          <Card>
            <Card.Header as="h5">Детали заказа</Card.Header>
            <Card.Body>
              <Form>
                {/* Номер заказа */}
                <Form.Group className="mb-3" controlId="orderNumber">
                  <Form.Label>Номер заказа</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.orderNumber}
                  />
                </Form.Group>

                {/* Город отправителя */}
                <Form.Group className="mb-3" controlId="senderCity">
                  <Form.Label>Город отправителя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.senderCity}
                  />
                </Form.Group>

                {/* Адрес отправителя */}
                <Form.Group className="mb-3" controlId="senderAddress">
                  <Form.Label>Адрес отправителя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.senderAddress}
                  />
                </Form.Group>

                {/* Город получателя */}
                <Form.Group className="mb-3" controlId="receiverCity">
                  <Form.Label>Город получателя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.receiverCity}
                  />
                </Form.Group>

                {/* Адрес получателя */}
                <Form.Group className="mb-3" controlId="receiverAddress">
                  <Form.Label>Адрес получателя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.receiverAddress}
                  />
                </Form.Group>

                {/* Вес груза */}
                <Form.Group className="mb-3" controlId="weight">
                  <Form.Label>Вес груза (кг)</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={`${data.weight} кг`}
                  />
                </Form.Group>

                {/* Дата забора груза */}
                <Form.Group className="mb-3" controlId="pickupDate">
                  <Form.Label>Дата забора груза</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.pickupDate}
                  />
                </Form.Group>
              </Form>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default CurrentOrder;