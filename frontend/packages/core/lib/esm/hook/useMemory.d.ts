export declare const useMemory: () => {
    set: (key: string, value: unknown) => void;
    get: <T>(key: string) => T | null;
    remove: (key: string) => void;
};
