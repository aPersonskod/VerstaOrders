export const orderServiceBaseAddress = `http://localhost:5259/Order`;

export const fetchOrders = async (setOrders, setError, setLoading) => {
    try {
        let query = `${orderServiceBaseAddress}/GetOrders`;
        let options = {
            method: 'GET'
        }
        const response = await fetch(query, options);
        if (!response.ok) {
            let localError = await response.json();
            throw new Error(`Status code: ${localError.status}, error: ${localError.detail}`);
        }
        const orders = await response.json();
        setOrders(orders);
    } catch (err) {
        setError(err);
    } finally {
        setLoading(false);
    }
}

export const createOrder = async (formData, setError, setSubmitted) => {
    try {
        let query = `${orderServiceBaseAddress}`;
        let options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    "townSender": formData.senderCity,
                    "addressSender": formData.senderAddress,
                    "townReceiver": formData.receiverCity,
                    "addressReceiver": formData.receiverAddress,
                    "productWeight": formData.weight,
                    "pickupDate": formData.pickupDate
                }
            ),
        }
        const response = await fetch(query, options);
        if (!response.ok) {
            let localError = await response.json();
            throw new Error(`Status code: ${localError.status}, error: ${localError.detail}`);
        }
        setSubmitted(true);
    } catch (err) {
        setError(err);
    }
}

export const fetchOrder = async (orderId, setData, setError, setLoading) => {
    try {
        let query = `${orderServiceBaseAddress}/${orderId}`;
        let options = {
            method: 'GET'
        }
        const response = await fetch(query, options);
        if (!response.ok) {
            let localError = await response.json();
            throw new Error(`Status code: ${localError.status}, error: ${localError.detail}`);
        }
        const result = await response.json();
        setData(result);
    } catch (err) {
        setError(err);
    } finally {
        setLoading(false);
    }
}