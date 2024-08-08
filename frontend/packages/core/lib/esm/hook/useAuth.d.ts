import { Token } from '../type';
export declare const useAuth: () => {
    saveToken: (token: Token) => void;
    check: () => void;
    callback: () => void;
};
