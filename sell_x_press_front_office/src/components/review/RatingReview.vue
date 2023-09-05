<template>
    <div class="rating-container">
        <div class="star-rating-container">
            <div v-for="index  in product_rating" :key="index">
                <img src="../../assets/Product/yellow_star.png" alt="yellow stars" />
            </div>
            <div v-for="index in (5 - product_rating)" :key="index">
                <img src="../../assets/Product/star.png" alt="stars" />
            </div>
            <span class="rating">{{ product_rating }} out of 5 </span>
        </div>
        <div class="number-review">{{ number_review }} customer review</div>
        <div v-for="(value, index) in 5" class="ratings-table">
            <span>{{ 5 - (index) }} star</span>
            <div class="level-rating">
                <div class="level-rating"
                    :style="{ backgroundColor: '#F17720', width: Math.floor((star_rating[5 - index] / (number_review)) * 100) + '%' }">
                </div>
            </div>
            <span>{{ Math.floor((star_rating[5 - index] / (number_review)) * 100) }}%</span>
        </div>
    </div>
</template>

<script>
export default {
    name: 'RatingReview',
    props: {
        star_rating: {
            type: Object,
            required: true,
        },
        number_review: {
            type: Number,
            required: true,
        },
        product_rating: {
            type: Number,
            required: true,
        },
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