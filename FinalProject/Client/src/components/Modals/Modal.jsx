import VaadinClose from '../Icons/VaadinClose'
import React from 'react';

const Modal = ({ children, isOpen, close }) => {
    return (
        <div className={`${isOpen ? "translate-y-0" : "-translate-y-full"} duration-200 z-50 transition-all block fixed bg-opacity-80 inset-0 bg-black`}>
            <div className='relative block max-w-xl px-16 py-10 mx-auto mt-20 bg-gray-800'>
                {children}
                <button onClick={close} className='absolute text-2xl text-white top-2 right-2'>
                    <VaadinClose />
                </button>
            </div>
        </div>
    );
};

export default Modal;