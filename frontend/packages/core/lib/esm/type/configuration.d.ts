import { Token } from './auth';
export type Configuration = {
    dictionaries: Record<string, Record<string, string>>;
    defaultDictionary: string;
    baseUrl: string;
    authUrl?: string;
    token?: Token;
};
