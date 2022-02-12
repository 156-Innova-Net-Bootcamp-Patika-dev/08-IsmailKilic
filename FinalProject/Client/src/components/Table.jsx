import React from 'react'

const Table = ({ titles, children }) => {
    return (
        <table className="mt-12 shadow-2xl w-full border-collapse text-center table-fixed">
            <thead className='h-10 bg-[#37304A] text-white'>
                <tr>
                    {
                        titles.map((title, index) => (
                            <th key={index}>{title}</th>
                        ))
                    }
                </tr>
            </thead>
            <tbody>
                {children}
            </tbody>
        </table>
    )
}

export default Table