<template>
    <div class="rating-container">
        <div class="star-rating-container">
            <div v-for="index  in product_rating" :key="index">
                <img src="../../assets/Product/yellow_star.png" alt="yellow stars" />
            </div>
            <div v-for="index in (5 - product_rating)" :key="index">
                <img src="../../assets/Product/star.png" alt="stars" />
            </div>
            <span class="rating">{{ product_rating }} out of 5</span>
        </div>
        <div class="number-review">{{ number_review }} customer review</div>
        <div v-for="(value, index) in 5" class="ratings-table">
            <span>{{ 5 - (index) }} star</span>
            <div class="level-rating">
                <div class="level-rating"
                    :style="{ backgroundColor: '#F17720', width: Math.floor((star_rating[5 - index] / (number_review)) * 100) + '%' }">
                </div>
            </div>
            <span>{{Math.floor((star_rating[5 - index] / (number_review)) * 100) }}%</span>
        </div>
    </div>
</template>

<script>
export default {
    name: 'RatingReview',
    props: {
        comments: {
            type: Array,
            required: true,
        },
    },
    data() {
        return {
            number_review: 0,
            product_rating: 0,
            star_rating: {
                5: 0,
                4: 0,
                3: 0,
                2: 0,
                1: 0,
            },
        }
    },
    methods: {
        calculateProductRating() {
            let totalStars = 0;
            let totalReviews = 0;

            for (let i = 1; i <= 5; i++) {
                totalStars += i * this.star_rating[i];
                totalReviews += this.star_rating[i];
            }

            if (totalReviews > 0) {
                this.product_rating = Math.floor(totalStars / totalReviews);
            } else {
                this.product_rating = 0;
            }
        },
    },
    mounted() {
        if (this.comments) {
            this.number_review = this.comments.length;
            for (let i = 0; i < this.comments.length; i++) {
                switch (this.comments[i].mark.rate) {
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
                };
            };
            this.calculateProductRating();
        };
    },
}
</script>

<style scoped>
.rating-container {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    gap: 0.5rem;
}

.star-rating-container {
    display: flex;
    flex-wrap: wrap;
    gap: 0.5rem;
    align-items: flex-end;
}

.star-rating-container img {
    height: 2rem;
}

.ratings-table {
    display: flex;
    flex-direction: row;
    align-items: center;
    gap: 1rem;
}

.level-rating {
    border: none;
    background-color: var(--main-grey-separation);
    height: 2vh;
    width: 12vw;
}

.rating {
    margin-left: 1rem;
    font-size: 1.5rem;
}

.number-review {
    color: var(--main-grey-separation);
    font-size: 1rem;
}
</style>