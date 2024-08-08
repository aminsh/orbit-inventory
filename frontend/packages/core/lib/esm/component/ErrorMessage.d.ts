import type { FC } from 'react';
export type ErrorMessageProps = {
    title: string;
    message: string | string[];
    onClose?: () => void;
};
export declare const ErrorMessage: FC<ErrorMessageProps>;
