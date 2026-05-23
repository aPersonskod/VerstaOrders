import { useState, useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import { useParams } from "react-router";
import { Form, Container, Row, Col, Card, Button } from 'react-bootstrap';
import Loading from './Loading';
import Error from './Error';
import { fetchOrder } from "../OrderApiHelper.jsx";

const CurrentOrder = () => {
  const navigate = useNavigate();
  let { orderId } = useParams();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const defaultOrder = {
    orderNumber: 'ЗАКАЗ-001',
    townSender: 'Москва',
    addressSender: 'ул. Тверская, д. 1, оф. 5',
    townReceiver: 'Санкт-Петербург',
    addressReceiver: 'Невский пр., д. 10, кв. 12',
    productWeight: 15.5,
    pickupDate: '2025-03-25'
  };
  const [data, setData] = useState();

  useEffect(() => {
    fetchOrder(orderId, setData, setError, setLoading);
  }, []);

  if (loading) return <Loading/>;
  if (error) return <Container><Error message={error.message}/></Container>;

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
                    value={data.townSender}
                  />
                </Form.Group>

                {/* Адрес отправителя */}
                <Form.Group className="mb-3" controlId="senderAddress">
                  <Form.Label>Адрес отправителя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.addressSender}
                  />
                </Form.Group>

                {/* Город получателя */}
                <Form.Group className="mb-3" controlId="receiverCity">
                  <Form.Label>Город получателя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.townReceiver}
                  />
                </Form.Group>

                {/* Адрес получателя */}
                <Form.Group className="mb-3" controlId="receiverAddress">
                  <Form.Label>Адрес получателя</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={data.addressReceiver}
                  />
                </Form.Group>

                {/* Вес груза */}
                <Form.Group className="mb-3" controlId="weight">
                  <Form.Label>Вес груза (кг)</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={`${data.productWeight} кг`}
                  />
                </Form.Group>

                {/* Дата забора груза */}
                <Form.Group className="mb-3" controlId="pickupDate">
                  <Form.Label>Дата забора груза</Form.Label>
                  <Form.Control
                    type="text"
                    readOnly
                    value={new Date(data.pickupDate).toLocaleString('en-GB', { timeZone: 'UTC', day: '2-digit', month: '2-digit', year: 'numeric' }).replace(/\//g, '.')}
                  />
                </Form.Group>
              </Form>
            </Card.Body>
          </Card>
          <br/>
          <Button variant='secondary' onClick={() => {navigate('/');}}>Назад</Button>
        </Col>
      </Row>
    </Container>
  );
}

export default CurrentOrder;