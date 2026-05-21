import { useState, useEffect } from "react";
import { Container, Row, Col, Card } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import {ApiHelper} from "../ApiHelper.jsx";

const AllOrders = () => {
  const navigate = useNavigate();
  const apiHelper = new ApiHelper();
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchOrders = async () => {
    try {
        let query = `${apiHelper.orderServiceBaseAddress}/GetOrders`;
        let options = {
            method: 'GET'
        }
        const response = await fetch(query, options);
        if (!response.ok) {
            let localError = await response.json();
            throw new Error(localError.error);
        }
        const result = await response.json();
        setOrders(result);
    } catch (err) {
        setError(err);
    } finally {
        setLoading(false);
    }
  }

  useEffect(() => {
    fetchOrders();
  }, []);

  const ordersLocal = [
  {
    id: '1',
    orderNumber: 'ORD-001',
    senderCity: 'Москва',
    senderAddress: 'ул. Тверская, д. 10',
    receiverCity: 'Санкт-Петербург',
    receiverAddress: 'Невский пр., д. 25',
    weight: 15.5,
  },
  {
    id: '2',
    orderNumber: 'ORD-002',
    senderCity: 'Казань',
    senderAddress: 'ул. Баумана, 5',
    receiverCity: 'Екатеринбург',
    receiverAddress: 'пр. Ленина, 100',
    weight: 8.2,
  },
  {
    id: '3',
    orderNumber: 'ORD-003',
    senderCity: 'Новосибирск',
    senderAddress: 'Красный пр., 12',
    receiverCity: 'Владивосток',
    receiverAddress: 'Океанский пр., 7',
    weight: 42.0,
  },
  ];

  // Обработчик клика по карточке – переход на страницу заказа
  const handleCardClick = (orderId) => {
    // Пример маршрута: /order/123
    navigate(`/order/${orderId}`);
    // Можно также передать state: navigate(`/order/${orderId}`, { state: { order } });
  };

  // Если список заказов пуст – показываем сообщение
  if (!orders.length) {
    return (
      <Container className="mt-4">
        <p className="text-center">Нет доступных заказов</p>
      </Container>
    );
  }

  if (loading) return <div>Loading data...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <Container className="mt-4">
      <h2 className="mb-4">Список заказов</h2>
      <Row xs={1} md={2} lg={3} className="g-4">
        {orders.map((order) => (
          <Col key={order.orderId}>
            <Card
              className="h-100 shadow-sm order-card"
              onClick={() => handleCardClick(order.orderId)}
              style={{ cursor: 'pointer', transition: 'transform 0.2s' }}
              onMouseEnter={(e) =>
                (e.currentTarget.style.transform = 'translateY(-5px)')
              }
              onMouseLeave={(e) =>
                (e.currentTarget.style.transform = 'translateY(0)')
              }
            >
              <Card.Body>
                <Card.Title className="mb-3">
                  Заказ № {order.orderNumber}
                </Card.Title>
                <Card.Text>
                  <strong>Город отправителя:</strong> {order.townSender}
                  <br />
                  <strong>Адрес отправителя:</strong> {order.addressSender}
                  <br />
                  <strong>Город получателя:</strong> {order.townReceiver}
                  <br />
                  <strong>Адрес получателя:</strong> {order.addressReceiver}
                  <br />
                  <strong>Вес груза:</strong> {order.productWeight} кг
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
}

export default AllOrders;