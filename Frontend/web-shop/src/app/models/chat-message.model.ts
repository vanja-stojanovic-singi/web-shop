export interface ChatMessage {
    sender: string;
    message: string;
    sentMessageFlg: boolean;
}

export interface ChatBotResponse {
    recipient_id: string;
    text: string;
}