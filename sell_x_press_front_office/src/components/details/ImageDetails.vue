<template>
    <div class="img-publication-container">
        <div class="img-upload" id="input-upload-file" name="input-upload-file">
            <img :src="product.picture" alt="Product Image" />
        </div>
        <p class="publication_date">Published on: {{ formatCreatedAt(product.createdAt) }}</p>
    </div>
</template>

<script>

import { format } from 'date-fns';

export default {
    name: 'ImageDetails',
    props: {
        product: {
            type: Object,
            required: true
        },
    },
    methods: {
        formatCreatedAt(createdAt) {
            if (createdAt) return format(new Date(createdAt), 'dd/MM/yyyy');
            else return '';
        },
    },
}
</script>

<style scoped>

.img-publication-container {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}

.publication_date {
    text-align: end;
    margin-right: 0.5rem;
    color: var(--main-grey-separation);
}

.img-upload {
    width: 435px;
    height: 436px;
    border-radius: 1rem;
    border: 1px var(--main-black) solid;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.img-upload img {
    max-width: 100%;
    max-height: 100%;
    border-radius: 1rem;
}

.upload-file {
    display: flex;
    flex-direction: row;
    align-items: baseline;
    justify-content: flex-end;
    margin-top: 0.5rem;
    padding-right: 1rem;
    gap: 1rem;
    width: 100%;
    cursor: pointer;
}

.upload-file p {
    font-size: 15px;
    color: var(--main-black);
    font-weight: bold;
}

.upload-file p:hover {
    text-decoration: underline;
}

.upload-file img {
    height: 2rem;
}

</style>