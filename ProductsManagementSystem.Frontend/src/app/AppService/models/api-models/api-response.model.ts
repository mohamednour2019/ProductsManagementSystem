export interface ApiResponse<T> {
    isValidatableResponse: boolean
    data: T;
    Messages: string[]
}