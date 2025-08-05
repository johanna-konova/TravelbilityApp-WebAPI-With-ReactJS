import Pagination from 'react-bootstrap/Pagination';

import styles from './All-Properties.module.css';

export default function Paginator({ currentPage, totalCount, itemsPerPage, updatecurrentPageNumber }) {
    const totalPages = Math.ceil(totalCount / itemsPerPage);

    if (totalPages <= 1) return null;

    const pages = [];

    for (let i = 1; i <= totalPages; i++) {
        pages.push(
            <Pagination.Item
                key={i}
                linkClassName={i === currentPage ? styles["active-page"] : ""}
                disabled={i === currentPage}
                onClick={() => updatecurrentPageNumber(i)}
            >
                {i}
            </Pagination.Item>
        );
    }

    return (
        <Pagination>
            <Pagination.Item
                onClick={() => currentPage > 1 && updatecurrentPageNumber(currentPage - 1)}
                disabled={currentPage === 1}
            >
                {"< Previous"}
            </Pagination.Item>

            {pages}

            <Pagination.Item
                onClick={() => currentPage < totalPages && updatecurrentPageNumber(currentPage + 1)}
                disabled={currentPage === totalPages}
            >
                {"Next >"}
            </Pagination.Item>
        </Pagination>
    );
}
