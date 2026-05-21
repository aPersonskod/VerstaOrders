import { useNavigate } from "react-router-dom";
import { useState } from "react";
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Col, Row } from "react-bootstrap";

const Header = () => {
    const navigate = useNavigate();
    const companyName = `Верста`;
    const createOrder = () => {
        navigate("createOrder")
    }
    return(
        <>
        <Navbar className="bg-body-tertiary">
            <Container>
                <Navbar.Brand href="/">
                    <img
                    alt="logo"
                    src="./logo.png"
                    width="35"
                    height="35"
                    className="d-inline-block align-top"
                    />{' '}
                    {companyName}
                </Navbar.Brand>
                <Navbar.Toggle />
                <Navbar.Collapse className="justify-content-end">
                    <Button type="submit" onClick={createOrder}>Создать заказ</Button>
                </Navbar.Collapse>
            </Container>
        </Navbar>
        </>
    );
};

const her = () => {
   const navigate = useNavigate();
    const companyName = `Верста`;
    const expand = 'md';
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [user, setUser] = useState({});
    const [screenSize, setScreenSize] = useState({
        width: window.innerWidth,
        height: window.innerHeight,
    });
    const createOrder = () => {
        navigate("order/1")
    }

    return (
        <>
        <div>
            <div>
                <Navbar key={expand} expand={expand} className="bg-body-tertiary mb-3"
                    style={{borderRadius: '21px', backgroundColor: '#ececec'}}>
                <Container fluid>
                    <Navbar.Brand href='/'>
                        <img 
                            src={"./logo.png"}
                            alt={companyName}
                            className="h-10 w-auto object-contain"
                            />
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls={`offcanvasNavbar-expand-${expand}`}/>
                    <Navbar.Offcanvas
                        id={`offcanvasNavbar-expand-${expand}`}
                        aria-labelledby={`offcanvasNavbarLabel-expand-${expand}`}
                        placement="end"
                    >
                        <Offcanvas.Header closeButton>
                            <Offcanvas.Title id={`offcanvasNavbarLabel-expand-${expand}`}>
                                {companyName}
                            </Offcanvas.Title>
                        </Offcanvas.Header>
                        <Offcanvas.Body>
                            <Nav className="justify-content-start flex-grow-1 pe-3">
                                <Nav.Link href="/">Home</Nav.Link>
                                <Nav.Link href="/purchases">History</Nav.Link>
                            </Nav>
                {/*             <NavDropdown
                                drop={screenSize.width < 768 ? 'down-centered' : 'start'}
                                title={isLoggedIn ? <LoggedLogo/> : <NotLoggedLogo/>}
                                id={`offcanvasNavbarDropdown-expand-${expand}`}
                            >
                                {isLoggedIn && <NavDropdown.Item>{user.name}</NavDropdown.Item>}
                                {isLoggedIn && <NavDropdown.Item>Баланс: {user.wallet}</NavDropdown.Item>}
                                {
                                    isLoggedIn &&
                                    <NavDropdown.Item onClick={walletReplenishment}>
                                        Пополнить баланс
                                    </NavDropdown.Item>
                                }
                                {isLoggedIn && <NavDropdown.Divider/>}
                                {
                                    isLoggedIn &&
                                    <NavDropdown.Item onClick={logOutHandler}>
                                        Выход
                                    </NavDropdown.Item>
                                }
                                {!isLoggedIn && <Button onClick={() => {navigate('/auth');}}>Войти</Button>}
                            </NavDropdown> */}
                        </Offcanvas.Body>
                    </Navbar.Offcanvas>
                </Container>
            </Navbar>
            </div>
{/*             <header className="bg-white shadow-md p-4 flex flex-wrap justify-between items-center">
            <div className="flex items-center space-x-2">
                <img 
                src={"./logo.png"}
                alt={companyName}
                className="h-10 w-auto object-contain"
                />
                <span className="text-xl font-bold text-gray-800">
                {companyName}
                </span>
            </div>

            <button
                onClick={createOrder}
                className="bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded transition duration-200 ease-in-out mt-2 sm:mt-0 focus:outline-none focus:ring-2 focus:ring-blue-400 focus:ring-opacity-75"
            >
                Create Order
            </button>
            </header> */}
        </div>
        </>
    );
}

export default Header;