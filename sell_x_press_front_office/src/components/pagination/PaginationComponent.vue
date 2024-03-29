<template>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-center">
            <li class="page-item" :class="{ disabled: currentPage === 1 }">
                <a class="page-link previous" href="#" tabindex="-1" aria-disabled="true"
                    @click="gotoPage(currentPage - 1)">Previous</a>
            </li>
            <li class="page-item" v-for="page in displayedPages" :key="page">
                <a class="page-link" href="#products" @click="gotoPage(page)">{{ page }}</a>
            </li>
            <li v-if="showEllipsisBefore">
                <span class="page-link">...</span>
            </li>
            <li class="page-item" v-if="showLastPage">
                <a class="page-link" href="#products" @click="gotoPage(totalPages)">{{ totalPages }}</a>
            </li>
            <li class="page-item" :class="{ disabled: currentPage === totalPages }">
                <a class="page-link next" href="#products" @click="gotoPage(currentPage + 1)">Next</a>
            </li>
        </ul>
    </nav>
</template>
  
<script>
export default {
    name: 'PaginationComponent',
    props: {
        totalProducts: {
            type: Number,
            required: true,
        },
        products: {
            type: Array,
            required: true,
        },
        productsPerPage: {
            type: Number,
            required: true,
        },
        currentPage: {
            type: Number,
            required: true,
        }
    },
    computed: {
        totalPages() {
            return Math.ceil(this.totalProducts / this.productsPerPage)
        },
        displayedPages() {
            const visiblePages = Math.min(this.totalPages, 10);
            const pages = [];

            if (this.totalPages <= 10) {
                for (let page = 1; page <= this.totalPages; page++) {
                    pages.push(page);
                }
            } else if (this.currentPage <= 4) {
                for (let page = 1; page <= 3; page++) {
                    pages.push(page);
                }
            } else if (this.currentPage >= this.totalPages - 3) {
                for (let page = this.totalPages - 2; page <= this.totalPages; page++) {
                    pages.push(page);
                }
            } else {
                for (let offset = -1; offset <= 1; offset++) {
                    pages.push(this.currentPage + offset);
                }
            }

            return pages;
        },
        showEllipsisBefore() {
            return this.totalPages > 10 && this.currentPage > 4;
        },
        showLastPage() {
            return this.totalPages > 3 && this.currentPage < this.totalPages - 2;
        },
    },
    methods: {
        gotoPage(page) {
            if (page >= 1 && page <= this.totalPages) {
                this.$emit('page-changed', page);
            }
        }
    },
};
</script> 

<style scoped>

.page-item:first-child .page-link {
    border-top-left-radius: 1rem;
    border-bottom-left-radius: 1rem;
}

.page-item:last-child .page-link {
    border-top-right-radius: 1rem !important;
    border-bottom-right-radius: 1rem !important;
}

</style>