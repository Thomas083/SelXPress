<template>
    <div class="product-details-container">
        <div class="img-details-container">
            <image-details :product="product" />
            <product-details :product="product" />
        </div>
        <div class="separation-line"></div>
        <description-details :description="product.description" />
        <div class="separation-line"></div>
        <div class="customer-review-container">
            <h1>Customer Review</h1>
            <div class="rating-review-container">
                <rating-review :comments="product.comments"/>
                <customer-review :comments="product.comments" :productId="product.id"/>
            </div>
        </div>
        
    </div>
</template>

<script>

import { GET } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";
import ImageDetails from "@/components/details/ImageDetails.vue";
import ProductDetails from "@/components/details/ProductDetails.vue";
import DescriptionDetails from "@/components/details/DescriptionDetails.vue";
import RatingReview from "@/components/review/RatingReview.vue";
import CustomerReview from "@/components/review/CustomerReview.vue";

export default {
    name: 'ProductDetailsView',
    components: {
        ImageDetails,
        ProductDetails,
        DescriptionDetails,
        RatingReview,
        CustomerReview,
    },
    data() {
        return {
            product: null,
        }
    },
    computed: {
        product() {
            return this.product ? this.product : '';
        }
    },
    mounted() {
        GET(ENDPOINTS.GET_ONE_PRODUCT + this.$route.params.id)
            .then((response) => {
                this.product = response.data;
            })
            .catch((error) => {
                console.dir(error);
            });
    }
}

</script>

<style scoped>
.product-details-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    padding: 3rem;
    margin: 1rem;
    background-color: var(--main-white);
    border-radius: 1rem;
}

.img-details-container {
    display: flex;
    flex-direction: row;
    gap: 1rem;
}

@media screen and (max-width: 1023px) {
    .img-details-container {
        flex-wrap: wrap;
    }

    .rating-review-container {
        flex-wrap: wrap;
    }
}

.separation-line {
    border: 1px var(--main-grey-separation) solid;
    width: 100%;
}

img {
    height: 3rem;
}

.customer-review-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    align-items: flex-start;
}

.rating-review-container {
    display: flex;
    flex-direction: row;
    gap: 3rem;
}

</style>