import React from 'react'
import DataTable from 'react-data-table-component'

const customStyles = {
  head: {
    style: {
      color: '#fff',
      fontSize: '15px',
      fontWeight: 600,
    },
  },
  headRow: {
    style: {
      backgroundColor: '#37304A',
      minHeight: '52px',
    },
  },
  rows: {
    style: {
      fontSize: '14px',
      fontWeight: 400,
      minHeight: '48px',
    },
  }
};

const paginationComponentOptions = {
  rowsPerPageText: 'Sayfa başına kayıt',
  rangeSeparatorText: 'de',
};

const MyDataTable = ({ columns, data }) => {
  return (
    <DataTable
      striped
      pagination
      paginationComponentOptions={paginationComponentOptions}
      paginationPerPage={5}
      paginationRowsPerPageOptions={[5, 10, 15]}
      noDataComponent={<div className='py-5'>Kayıt Bulunamadı</div>}
      highlightOnHover
      customStyles={customStyles}
      dense
      columns={columns}
      data={data}
    />
  )
}

export default MyDataTable