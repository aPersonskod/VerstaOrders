export class ApiHelper {
    dev = (isHttps) => {
        return isHttps ? 'https://localhost:7' : 'http://localhost:5';
    };

    orderServiceBaseAddress = `${this.dev(false)}259/Order`;
}