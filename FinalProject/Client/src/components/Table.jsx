import React from 'react'

const Table = ({ titles, children }) => {
    return (
        <table className="w-full mt-12 text-center border-collapse shadow-2xl table-auto">
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