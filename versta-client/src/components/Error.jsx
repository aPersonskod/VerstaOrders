import { Container, Alert } from 'react-bootstrap';

const Error = ({message}) => {
    return(
        <>
            <Alert key="danger" variant="danger">
                {message}
            </Alert>
        </>
    );
}

export default Error;