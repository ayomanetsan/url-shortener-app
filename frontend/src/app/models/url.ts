import { Creator } from "./creator";

export interface Url {
    id: number,
    shortUrl: string,
    originalUrl: string,
    createdBy: Creator,
    createdAt: Date,
}