import { useState } from "react";

export const useLocalStorage = <T>(
  key: string,
  defaultValue: T
): [T, (newValue: T) => void] => {
  const [storedValue, setStoredValue] = useState<T>(() => {
    try {
      const value = window.localStorage.getItem(key);
      if (value) {
        return JSON.parse(value) as T;
      } else {
        window.localStorage.setItem(key, JSON.stringify(defaultValue));
        return defaultValue;
      }
    } catch (error) {
      return defaultValue;
    }
  });
  const setValue = (newValue: T) => {
    try {
      window.localStorage.setItem(key, JSON.stringify(newValue));
    } catch {
      throw new Error(
        `${key}: ${JSON.stringify(
          newValue
        )} could not be stored in localStorage`
      );
    }
    setStoredValue(newValue);
  };
  return [storedValue, setValue];
};
