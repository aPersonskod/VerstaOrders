import { Container, Spinner } from 'react-bootstrap';

const Loading = () => {
    return(
        <>
            <Container>
                <Spinner animation="border" variant="primary" />
            </Container>
        </>
    );
}

export default Loading;