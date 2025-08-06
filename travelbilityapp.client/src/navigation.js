let setErrorFn;

export const setErrorHandler = (fn) => setErrorFn = fn;
export const triggerError = (err) => setErrorFn && setErrorFn(err);