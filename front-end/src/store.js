import { configureStore } from "@reduxjs/toolkit";
import contactSlice from "./redux-toolkit/contactSlice";

export const storeReducer = {
    contact: contactSlice,

}

export const store = configureStore({
    reducer: storeReducer
});