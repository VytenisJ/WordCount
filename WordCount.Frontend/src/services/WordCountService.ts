class WordCountService {
    post(text: string) {
        const requestOptions = {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ text: text })
        };
        return fetch("/WordCount", requestOptions);
    }
}

export default new WordCountService();