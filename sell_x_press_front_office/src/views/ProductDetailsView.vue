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
                <rating-review :star_rating="star_rating" :number_review="number_review" :product_rating="product_rating" />
                <!-- <customer-review :comments="product.comments" :productId="product.id"/> -->
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
            star_rating: {
                5: 0,
                4: 0,
                3: 0,
                2: 0,
                1: 0,
            },
            number_review: 0,
            product_rating: 0,
        }
    },
    methods: {
        calculateProductRating() {
            let totalStars = 0;
            let totalReviews = 0;

            Object.keys(this.star_rating).forEach((key) => {
                totalStars += Number(key) * this.star_rating[key];
                totalReviews += this.star_rating[key];
            });

            if (totalReviews > 0) {
                this.product_rating = Math.floor(totalStars / totalReviews);
            } else {
                this.product_rating = 0;
            }
        },
    },
    computed: {
        product() {
            return this.product ? this.product : '';
        }
    },
    created() {
        GET(ENDPOINTS.GET_ONE_PRODUCT + this.$route.params.id)
            .then((response) => {
                this.product = response.data;
                this.number_review = this.product.comments.length
                for (let i = 0; i < this.product.comments.length; i++) {
                    GET(ENDPOINTS.GET_ONE_COMMENT + `/${this.product.comments[i].id}`)
                        .then((response) => {
                            this.product.comments[i].mark = response.data.mark
                            switch (this.product.comments[i].mark.rate) {
                                case 5:
                                    this.star_rating[5] += 1;
                                    break;
                                case 4:
                                    this.star_rating[4] += 1;
                                    break;
                                case 3:
                                    this.star_rating[3] += 1;
                                    break;
                                case 2:
                                    this.star_rating[2] += 1;
                                    break;
                                case 1:
                                    this.star_rating[1] += 1;
                                    break;
                                default:
                                    break;
                            }
                            this.calculateProductRating()
                        })
                        .catch((error) => {
                            console.dir(error)
                        })
                };
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